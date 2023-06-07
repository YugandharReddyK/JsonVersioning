using System;
using System.Collections.Generic;

namespace ModelsGeneration.Models;

public partial class OdisseusToolCodeDatum
{
    public OdisseusToolCodeDatum()
    {

    }
    public Guid Id { get; set; }

    public byte[] Data { get; set; } = null!;
}
