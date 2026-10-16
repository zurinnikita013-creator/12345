using System;
using System.Collections.Generic;

namespace ClassLibrary.Models;

public partial class Film
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public short Duration { get; set; }

    public short ReleaseYear { get; set; }

    public string Description { get; set; } = null!;

    public byte[]? Poster { get; set; }

    public string? AgeRating { get; set; }

    public DateOnly? RentalStartDate { get; set; }

    public DateOnly? RentalEndDate { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
