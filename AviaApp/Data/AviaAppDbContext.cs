using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AviaAppDbContext : IdentityDbContext<AviaAppUser>
{
    public DbSet<Country> Countries { get; set; }

    public DbSet<City> Cities { get; set; }

    public DbSet<Airport> Airports { get; set; }

    public AviaAppDbContext(DbContextOptions<AviaAppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Country>()
            .HasMany(x => x.Cities)
            .WithOne(x => x.Country);

        builder.Entity<Country>()
            .HasIndex(x => x.Name)
            .IsUnique();

        builder.Entity<City>()
            .HasMany(x => x.Airports)
            .WithOne(x => x.City);
    }
}