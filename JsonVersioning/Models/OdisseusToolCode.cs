﻿using System;
using System.Collections.Generic;

namespace ModelsGeneration.Models;

public partial class OdisseusToolCode
{
    public OdisseusToolCode()
    {

    }
    public Guid Id { get; set; }

    public string FileName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsEnabled { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public string LastEditedBy { get; set; } = null!;

    public DateTime LastEditedTime { get; set; }
}
