using System;

namespace AviaApp.Models.Requests;

public class UpdateCityRequest
{
    public Guid CityId { get; set; }

    public string UpdatedCityName { get; set; }
}