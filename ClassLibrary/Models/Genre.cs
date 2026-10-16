using System;
using System.Collections.Generic;

namespace ClassLibrary.Models;

public partial class Genre
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
