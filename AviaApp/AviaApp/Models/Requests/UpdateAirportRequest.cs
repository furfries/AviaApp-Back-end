using System;

namespace AviaApp.Models.Requests;

public class UpdateAirportRequest
{
    public string UpdatedAirportName { get; set; }

    public Guid AirportId { get; set; }
}