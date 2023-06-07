﻿using System;
using System.Collections.Generic;

namespace ModelsGeneration.Models;

public partial class ObservatoryStation
{
    public ObservatoryStation()
    {

    }
    public Guid Id { get; set; }

    public string? Name1 { get; set; }

    public string? Description { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string? Date { get; set; }

    public DateTime LastUpdateTime { get; set; }

    public int Type { get; set; }

    public long OffsetTime { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ObservatoryReading> ObservatoryReadings { get; } = new List<ObservatoryReading>();

    public virtual ICollection<Solution> Solutions { get; } = new List<Solution>();
}
