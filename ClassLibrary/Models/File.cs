using System;
using System.Collections.Generic;

namespace ClassLibrary.Models;

public partial class File
{
    public int Id { get; set; }

    public byte[] File1 { get; set; } = null!;

    public string Name { get; set; } = null!;
}
