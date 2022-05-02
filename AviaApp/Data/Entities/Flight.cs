namespace Data.Entities;

public class Flight
{
    public Guid Id { get; set; }

    public Guid AirportFromId { get; set; }

    public Guid AirportToId { get; set; }

    public string Airplane { get; set; } = string.Empty;

    public DateTime DepartureDateTime { get; set; }

    public DateTime ArrivalDateTime { get; set; }

    public bool IsCanceled { get; set; }

    public decimal Price { get; set; }

    public Airport? AirportFrom { get; set; }

    public Airport? AirportTo { get; set; }

    public IList<Booking>? Bookings { get; set; }
}