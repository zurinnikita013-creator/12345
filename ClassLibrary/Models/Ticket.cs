using System;
using System.Collections.Generic;

namespace ClassLibrary.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public int VisitorId { get; set; }

    public int SessionId { get; set; }

    public byte RowNumber { get; set; }

    public byte SeatNumber { get; set; }

    public virtual Visitor Visitor { get; set; } = null!;
}
