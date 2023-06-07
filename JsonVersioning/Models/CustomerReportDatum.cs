using System;
using System.Collections.Generic;

namespace ModelsGeneration.Models;

public partial class CustomerReportDatum
{
    public CustomerReportDatum()
    {

    }
    public Guid Id { get; set; }

    public Guid WellId { get; set; }

    public byte[] ReportData { get; set; } = null!;
}
