using System;

namespace AviaApp.Models.Requests;

public class AddAirportRequest
{
    public Guid CityId { get; set; }

    public string AirportName { get; set; }
}