using AutoSzerelo.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoSzerelo.Api;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Ugyfel> Ugyfelek => Set<Ugyfel>();

    public DbSet<Munka> Munkak => Set<Munka>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ugyfel>().HasKey(ugyfel => ugyfel.Id);
        modelBuilder.Entity<Munka>().HasKey(munka => munka.Id);

        modelBuilder.Entity<Munka>()
            .HasOne(munka => munka.Ugyfel)
            .WithMany(ugyfel => ugyfel.Munkak)
            .HasForeignKey(munka => munka.UgyfelId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}