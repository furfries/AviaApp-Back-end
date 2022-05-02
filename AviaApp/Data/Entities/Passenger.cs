namespace Data.Entities;

public class Passenger
{
    public Guid Id { get; set; }

    public Guid BookingId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public bool IsCanceled { get; set; }

    public DateTime? CancelDate { get; set; }

    public Booking Booking { get; set; }
}