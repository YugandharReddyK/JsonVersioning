using System;
using System.Collections.Generic;

namespace ModelsGeneration.Models;

public partial class TraceLog
{
    public TraceLog()
    {

    }
    public Guid Id { get; set; }

    public string? Message { get; set; }

    public DateTime Time { get; set; }
}
