using System;

namespace AviaApp.Models.Dto;

public class AirportDto : LocationBase
{
    public Guid CityId { get; set; }

    public CityDto City { get; set; }
}