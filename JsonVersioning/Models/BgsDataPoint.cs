using System;
using System.Collections.Generic;

namespace ModelsGeneration.Models;

public partial class BgsDataPoint
{
    public Guid Id { get; set; }

    public Guid CorrectedSurveyId { get; set; }

    public double? Inclination { get; set; }

    public double? Declination { get; set; }

    public double? TotalIntensity { get; set; }

    public double? Longitude { get; set; }

    public double? Latitude { get; set; }

    public double? Tvdss { get; set; }

    public DateTime? Date { get; set; }

    public virtual CorrectedSurvey CorrectedSurvey { get; set; } = null!;
    public BgsDataPoint()
    {

    }
}
