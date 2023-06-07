using System;
using System.Collections.Generic;

namespace ModelsGeneration.Models;

public partial class CustomerReport
{
    public CustomerReport()
    {
            
    }
    public Guid Id { get; set; }

    public Guid WellId { get; set; }

    public DateTime CreatedTime { get; set; }

    public string LastEditedBy { get; set; } = null!;

    public DateTime LastEditedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public Guid? TemplateId { get; set; }
}
