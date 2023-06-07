using System;
using System.Collections.Generic;

namespace ModelsGeneration.Models;

public partial class AdminUser
{
    public Guid Id { get; set; }

    public string? NetworkId { get; set; }

    public bool IsDeletable { get; set; }

    public byte? Status { get; set; }

    public string? LastEditedBy { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime LastEditedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    public string? EmailId { get; set; }

    public AdminUser()
    {

    }
}
