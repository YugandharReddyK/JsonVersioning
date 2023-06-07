﻿using System;
using System.Collections.Generic;

namespace ModelsGeneration.Models;

public partial class MaxSurveyActionResult
{
    public MaxSurveyActionResult()
    {

    }
    public Guid Id { get; set; }

    public Guid MaxSurveyRuleSetResponseId { get; set; }

    public string? ActionName { get; set; }

    public int? Result { get; set; }

    public string? Message { get; set; }

    public bool Deleted { get; set; }
}
