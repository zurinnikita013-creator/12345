using System;
using System.Collections.Generic;

namespace ClassLibrary.Models;

public partial class Visitor
{
    public int Id { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? Name { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
