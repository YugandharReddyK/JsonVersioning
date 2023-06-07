using System;
using System.Collections.Generic;

namespace ModelsGeneration.Models;

public partial class CustomerImage
{
    public CustomerImage()
    {

    }
    public Guid Id { get; set; }

    public byte[] Data { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public string ContentType { get; set; } = null!;

    public Guid WellId { get; set; }
}
