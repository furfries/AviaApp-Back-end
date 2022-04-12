using System;

namespace AviaApp.Models.Requests;

public class AddFlightRequest
{
    public Guid AirportFromId { get; set; }

    public Guid AirportToId { get; set; }

    public DateTime DepartureDateTime { get; set; }

    public DateTime ArrivalDateTime { get; set; }

    public string Airplane { get; set; }
}