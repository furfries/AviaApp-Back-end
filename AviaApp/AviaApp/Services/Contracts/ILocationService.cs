using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AviaApp.Models.Dto;

namespace AviaApp.Services.Contracts;

public interface ILocationService
{
    Task<IList<CountryDto>> GetCountriesAsync();

    Task<CountryDto> GetCountryByIdAsync(Guid id);

    Task AddCountryAsync(string countryName);

    Task UpdateCountryNameAsync(CountryDto country);
}