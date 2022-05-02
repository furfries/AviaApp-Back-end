using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Configurations;

public class BookingConfiguration
{
    public static void Create(ModelBuilder builder)
    {
        var priceConverter = new ValueConverter<decimal, double>(
            v => (double)v,
            v => (decimal)v
        );

        builder.Entity<Booking>()
            .HasMany(x => x.Passengers)
            .WithOne(x => x.Booking)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.Entity<Booking>()
            .Property(x => x.Price)
            .HasPrecision(15);

        builder
            .Entity<Booking>()
            .Property(e => e.Price)
            .HasConversion(priceConverter);
    }
}