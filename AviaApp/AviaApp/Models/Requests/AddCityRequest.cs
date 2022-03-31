using System;

namespace AviaApp.Models.Requests;

public class AddCityRequest
{
    public Guid CountryId { get; set; }

    public string CityName { get; set; }
}