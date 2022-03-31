using System;

namespace AviaApp.Models.Dto;

public class CityDto
{
    public Guid Id { get; set; }

    public Guid CountryId { get; set; }

    public string Name { get; set; }

    public CountryDto Country { get; set; }
}