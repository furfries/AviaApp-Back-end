using System;

namespace AviaApp.Models.Dto;

public class FlightDto
{
    public Guid Id { get; set; }

    public string AirPlane { get; set; }

    public DateTime FlightDateTime { get; set; }

    public AirportDto AirportFrom { get; set; }

    public AirportDto AirportTo { get; set; }
}