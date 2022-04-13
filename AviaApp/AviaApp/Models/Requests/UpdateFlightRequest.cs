using System;

namespace AviaApp.Models.Requests;

public class UpdateFlightRequest
{
    public Guid FlightId { get; set; }

    public DateTime? DepartureDateTime { get; set; }

    public DateTime? ArrivalDateTime { get; set; }

    public string Airplane { get; set; }
}