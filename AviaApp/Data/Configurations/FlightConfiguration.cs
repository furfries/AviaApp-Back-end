using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Configurations;

public static class FlightConfiguration
{
    public static void Create(ModelBuilder builder)
    {
        var priceConverter = new ValueConverter<decimal, double>(
            v => (double)v,
            v => (decimal)v
        );

        builder.Entity<Flight>()
            .HasOne(x => x.AirportFrom)
            .WithMany()
            .HasForeignKey(x => x.AirportFromId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.Entity<Flight>()
            .HasOne(x => x.AirportTo)
            .WithMany()
            .HasForeignKey(x => x.AirportToId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.Entity<Flight>()
            .Property(x => x.Price)
            .HasPrecision(15);

        builder
            .Entity<Flight>()
            .Property(e => e.Price)
            .HasConversion(priceConverter);
    }
}