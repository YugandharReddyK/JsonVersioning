using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;

class Program
{
    static void Main()
    {
        string json1 = File.ReadAllText("C:\\Temp\\JsonVersioning\\JsonVersioning\\Well.json");
        string json2 = File.ReadAllText("C:\\Temp\\JsonVersioning\\JsonVersioning\\Well3Delete.json");

        JObject obj1 = JObject.Parse(json1);
        JObject obj2 = JObject.Parse(json2);

        List<ChangeInfo> changes = GetJsonChanges(obj1, obj2);

        foreach (var change in changes)
        {
            Console.WriteLine(change.ToString());
        }

        Console.ReadLine();
    }

    static List<ChangeInfo> GetJsonChanges(JObject obj1, JObject obj2)
    {
        List<ChangeInfo> changes = new List<ChangeInfo>();

        CompareJsonObjects(obj1, obj2, "", changes);

        return changes;
    }

    static void CompareJsonObjects(JObject obj1, JObject obj2, string currentPath, List<ChangeInfo> changes)
    {
        foreach (var property in obj2.Properties())
        {
            string propertyName = property.Name;
            if (propertyName != "$id" && propertyName != "$ref")
            {
                string propertyPath = string.IsNullOrEmpty(currentPath) ? propertyName : currentPath + "." + propertyName;

                if (obj1.TryGetValue(propertyName, out JToken value1))
                {
                    JToken value2 = property.Value;

                    if (!JToken.DeepEquals(value1, value2))
                    {
                        if (value2.Type == JTokenType.Object && value1.Type == JTokenType.Object)
                        {
                            CompareJsonObjects((JObject)value1, (JObject)value2, propertyPath, changes);
                        }
                        else if(value2.Type == JTokenType.Array && value1.Type == JTokenType.Array && propertyName != "RunsFilters")
                        {
                            CompareJsonArrays((JArray)value1, (JArray)value2, propertyPath, changes);
                        }
                        else
                        {
                            changes.Add(new ChangeInfo(propertyPath, ChangeType.Update, value1.ToString(), value2.ToString()));
                        }
                    }
                }
                else
                {
                    changes.Add(new ChangeInfo(propertyPath, ChangeType.Add, "", property.Value.ToString()));
                }
            }
        }

        foreach (var property in obj1.Properties())
        {
            string propertyName = property.Name;
            string propertyPath = string.IsNullOrEmpty(currentPath) ? propertyName : currentPath + "." + propertyName;

            if (!obj2.TryGetValue(propertyName, out _))
            {
                changes.Add(new ChangeInfo(propertyPath, ChangeType.Delete, property.Value.ToString(), ""));
            }
        }
    }

    static void CompareJsonArrays(JArray arr1, JArray arr2, string currentPath, List<ChangeInfo> changes)
    {
        int count1 = arr1.Count;
        int count2 = arr2.Count;

        int minLength = Math.Min(count1, count2);

        for (int i = 0; i < minLength; i++)
        {
            JToken item1 = arr1[i];
            JToken item2 = arr2[i];

            if (item1.Type == JTokenType.Object && item2.Type == JTokenType.Object)
            {
                CompareJsonObjects((JObject)item1, (JObject)item2, currentPath + "[" + i + "]", changes);
            }
            else if (item1.Type == JTokenType.Array && item2.Type == JTokenType.Array)
            {
                CompareJsonArrays((JArray)item1, (JArray)item2, currentPath + "[" + i + "]", changes);
            }
            else if (!JToken.DeepEquals(item1, item2))
            {
                changes.Add(new ChangeInfo(currentPath + "[" + i + "]", ChangeType.Update, item1.ToString(), item2.ToString()));
            }
        }

        if (count1 > count2)
        {
            for (int i = minLength; i < count1; i++)
            {
                changes.Add(new ChangeInfo(currentPath + "[" + i + "]", ChangeType.Delete, arr1[i].ToString(), ""));
            }
        }
        else if (count2 > count1)
        {
            for (int i = minLength; i < count2; i++)
            {
                changes.Add(new ChangeInfo(currentPath + "[" + i + "]", ChangeType.Add, "", arr2[i].ToString()));
            }
        }
    }

    static void UpdateChangeTypeInDatabase(List<ChangeInfo> changes)
    {
        string connectionString = "YourConnectionString";
        string updateQuery = "UPDATE YourTable SET ChangeType = @ChangeType WHERE Path = @Path";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                foreach (ChangeInfo change in changes)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@ChangeType", change.ChangeType.ToString());
                    command.Parameters.AddWithValue("@Path", change.Path);

                    command.ExecuteNonQuery();
                }
            }
        }

        Console.WriteLine("ChangeType updated in the database.");
    }
}

enum ChangeType
{
    Add,
    Delete,
    Update
}

class ChangeInfo
{
    public string Path { get; set; }
    public ChangeType ChangeType { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }

    public ChangeInfo(string path, ChangeType changeType, string oldValue, string newValue)
    {
        Path = path;
        ChangeType = changeType;
        OldValue = oldValue;
        NewValue = newValue;
    }

    public override string ToString()
    {
        return $"Path: {Path}, ChangeType: {ChangeType}, OldValue: {OldValue}, NewValue: {NewValue}";
    }
}
