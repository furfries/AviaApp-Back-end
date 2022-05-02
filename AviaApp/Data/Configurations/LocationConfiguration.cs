using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Configurations;

public static class LocationConfiguration
{
    public static void Create(ModelBuilder builder)
    {
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