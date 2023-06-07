using Microsoft.Data.SqlClient;
using ModelsGeneration.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Program
{
    static List<string> IgnoreProperties = new List<string> { "$id", "$ref", "RunsFilters", "SolutionTypes", "RigStaleNotification", "LastSurveyDepth", "SinceLastSurveyTime", "ProcessingStatus", "NewSurveysCount" };



    static void Main()
    {
        string json1 = File.ReadAllText("C:\\Temp\\JsonVersioning\\JsonVersioning\\Well.json");
        string json2 = File.ReadAllText("C:\\Temp\\JsonVersioning\\JsonVersioning\\Well2.json");

        var start = DateTime.Now;
        JObject obj1 = JObject.Parse(json1);
        JObject obj2 = JObject.Parse(json2);

        List<ChangeInfo> changes = GetJsonChanges(obj1, obj2);
        double difference = (DateTime.Now - start).TotalMilliseconds;

        List<string> updateQueries = GetSQLUpdateQueries(changes.Where(a => a.ChangeType == ChangeType.Update).ToList());

        Console.ReadLine();
    }

    private static List<string> GetSQLUpdateQueries(List<ChangeInfo> changeInfos)
    {
        List<string> queries = new List<string>();
        Dictionary<string, List<ChangeInfo>> tableBasedChangeInfos = new Dictionary<string, List<ChangeInfo>>();
        foreach(ChangeInfo changeInfo in changeInfos)
        {
            string[] pathSplit = changeInfo.Path.Split('.');
            string tableName = "Well";
            if (pathSplit.Length > 1)
            {
                tableName = pathSplit[pathSplit.Length - 2];
            }
            tableName += $"|{changeInfo.Id}";

            if (tableBasedChangeInfos.ContainsKey(tableName))
            {
                tableBasedChangeInfos[tableName].Add(changeInfo);
            }
            else
            {
                tableBasedChangeInfos.Add(tableName, new List<ChangeInfo> { changeInfo });
            }
        }
        foreach(KeyValuePair<string, List<ChangeInfo>> table in tableBasedChangeInfos)
        {
            queries.Add($@"UPDATE {table.Key.Split('|')[0]} SET {string.Join(", ", table.Value.Select(v => $"{v.Path.Split('.').Last()} = '{v.NewValue}'"))} WHERE Id = '{table.Key.Split('|')[1]}';");
        }
        return queries;
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
            if (!IgnoreProperties.Contains(propertyName))
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
                        else if(value2.Type == JTokenType.Array && value1.Type == JTokenType.Array)
                        {
                            CompareJsonArrays((JArray)value1, (JArray)value2, propertyPath, changes);
                        }
                        else
                        {
                            changes.Add(new ChangeInfo(propertyPath, ChangeType.Update, value1.ToString(), value2.ToString(), new Guid(obj2.Value<string>("Id"))));
                        }
                    }
                }
                else
                {
                    changes.Add(new ChangeInfo(propertyPath, ChangeType.Add, "", property.Value.ToString(), new Guid(property.Value.Value<string>("Id"))));
                }
            }
        }

        foreach (var property in obj1.Properties())
        {
            string propertyName = property.Name;
            string propertyPath = string.IsNullOrEmpty(currentPath) ? propertyName : currentPath + "." + propertyName;

            if (!obj2.TryGetValue(propertyName, out _))
            {
                changes.Add(new ChangeInfo(propertyPath, ChangeType.Delete, property.Value.ToString(), "", new Guid(property.Value.Value<string>("Id"))));
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
                CompareJsonObjects((JObject)item1, (JObject)item2, currentPath, changes);
            }
            else if (item1.Type == JTokenType.Array && item2.Type == JTokenType.Array)
            {
                CompareJsonArrays((JArray)item1, (JArray)item2, currentPath, changes);
            }
            else if (!JToken.DeepEquals(item1, item2))
            {
                changes.Add(new ChangeInfo(currentPath, ChangeType.Update, item1.ToString(), item2.ToString(), new Guid(item2.Value<string>("Id"))));
            }
        }

        if (count1 > count2)
        {
            for (int i = minLength; i < count1; i++)
            {
                changes.Add(new ChangeInfo(currentPath, ChangeType.Delete, arr1[i].ToString(), "", new Guid(arr1[i].Value<string>("Id"))));
            }
        }
        else if (count2 > count1)
        {
            for (int i = minLength; i < count2; i++)
            {
                changes.Add(new ChangeInfo(currentPath, ChangeType.Add, "", arr2[i].ToString(), new Guid(arr2[i].Value<string>("Id"))));
            }
        }
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
    public Guid Id { get; set; }

    public ChangeInfo(string path, ChangeType changeType, string oldValue, string newValue, Guid id)
    {
        Path = path;
        ChangeType = changeType;
        OldValue = oldValue;
        NewValue = newValue;
        Id = id;
    }

    public override string ToString()
    {
        return $"Path: {Path}, ChangeType: {ChangeType}, OldValue: {OldValue}, NewValue: {NewValue}";
    }
}
