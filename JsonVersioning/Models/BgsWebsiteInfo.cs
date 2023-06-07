using System;
using System.Collections.Generic;

namespace ModelsGeneration.Models;

public partial class BgsWebsiteInfo
{
    public Guid Id { get; set; }

    public string? Websiteurl { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string SiteStatus { get; set; } = null!;

    public string? Websitename { get; set; }

    public BgsWebsiteInfo()
    {

    }
}
