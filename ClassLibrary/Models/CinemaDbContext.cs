using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.Models;

public partial class CinemaDbContext : DbContext
{
    public CinemaDbContext()
    {
    }

    public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<File> Files { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Hall> Halls { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Visitor> Visitors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=mssql; Initial Catalog=ispp5501; Persist Security Info=True;User ID=ispp5501; Password=5501; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<File>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("File");

            entity.Property(e => e.File1).HasColumnName("File");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.ToTable("Film");

            entity.Property(e => e.AgeRating)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Duration).HasDefaultValue((short)90, "DF_Film_Duration");
            entity.Property(e => e.ReleaseYear).HasDefaultValueSql("(datepart(year,getdate()))", "DF_Film_ReleaseYear");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasMany(d => d.Genres).WithMany(p => p.Films)
                .UsingEntity<Dictionary<string, object>>(
                    "FilmGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .HasConstraintName("FK_Film_Genre_Genre"),
                    l => l.HasOne<Film>().WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Film_Genre_Film"),
                    j =>
                    {
                        j.HasKey("FilmId", "GenreId");
                        j.ToTable("Film_Genre");
                    });
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genre");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Hall>(entity =>
        {
            entity.ToTable("Hall");

            entity.HasIndex(e => new { e.Cinema, e.HallNumber }, "IX_Hall").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Cinema)
                .HasMaxLength(50)
                .HasDefaultValue("Макси", "DF_Hall_Cinema");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.ToTable("Session");

            entity.Property(e => e.Price)
                .HasDefaultValue(200m, "DF_Session_Price")
                .HasColumnType("decimal(4, 0)");
            entity.Property(e => e.StartDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())", "DF_Session_StartDate");

            entity.HasOne(d => d.Film).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Session_Film");

            entity.HasOne(d => d.Hall).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.HallId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Session_Hall");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("Ticket");

            entity.HasOne(d => d.Visitor).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.VisitorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Visitor");
        });

        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.ToTable("Visitor");

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
