using System;

namespace AviaApp.Models.Dto;

public class CityDto : LocationBase
{
    public Guid CountryId { get; set; }

    public CountryDto Country { get; set; }
}