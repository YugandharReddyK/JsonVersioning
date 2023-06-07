﻿using System;
using System.Collections.Generic;

namespace ModelsGeneration.Models;

public partial class MaxSurveyRule
{
    public MaxSurveyRule()
    {

    }
    public Guid Id { get; set; }

    public string? RuleName { get; set; }

    public Guid SolutionId { get; set; }

    public string? LastEditedBy { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastEditedTime { get; set; }

    public DateTime? CreatedTime { get; set; }

    public string? Lhs { get; set; }

    public string? Rhs { get; set; }

    public string? UnitSystem { get; set; }

    public bool IsMandatory { get; set; }

    public int? Operator { get; set; }

    public int? LogicalCondition { get; set; }

    public Guid PreConditionMapId { get; set; }

    public bool Deleted { get; set; }

    public bool? IsEnabled { get; set; }

    public bool? IsValidRule { get; set; }

    public virtual Solution Solution { get; set; } = null!;
}
