using System;
using Data.Entities;

namespace AviaApp.Models.Dto;

public class AirportDto
{
    public Guid Id { get; set; }

    public Guid CityId { get; set; }

    public string Name { get; set; }

    public City City { get; set; }
}