using System;
using System.Collections.Generic;

namespace ClassLibrary.Models;

public partial class Hall
{
    public byte Id { get; set; }

    public string Cinema { get; set; } = null!;

    public byte HallNumber { get; set; }

    public byte RowCount { get; set; }

    public byte SeatsPerRow { get; set; }

    public bool IsVip { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
