using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AviaApp.Models.Dto;

namespace AviaApp.Services.Contracts;

public interface ICountryService
{
    Task<IList<CountryDto>> GetCountriesAsync();

    Task<CountryDto> GetCountryByIdAsync(Guid countryId);

    Task AddCountryAsync(string countryName);

    Task UpdateCountryNameAsync(CountryDto updatedCountry);

    Task DeleteCountryAsync(Guid countryId);

    Task AssignCountryAsync(AirportDto airport);
}