using Data.Configurations;
using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AviaAppDbContext : IdentityDbContext<AviaAppUser>
{
    public DbSet<Country> Countries { get; set; }

    public DbSet<City> Cities { get; set; }

    public DbSet<Airport> Airports { get; set; }

    public DbSet<Flight> Flights { get; set; }

    public DbSet<CabinClass> CabinClasses { get; set; }

    public DbSet<Booking> Bookings { get; set; }

    public DbSet<Passenger> Passengers { get; set; }

    public AviaAppDbContext(DbContextOptions<AviaAppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        LocationConfiguration.Create(builder);
        FlightConfiguration.Create(builder);
        BookingConfiguration.Create(builder);
    }
}