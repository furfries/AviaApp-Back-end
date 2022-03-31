using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AviaApp.Models.Dto;
using AviaApp.Models.Requests;

namespace AviaApp.Services.Contracts;

public interface ICityService
{
    Task<IList<CityDto>> GetCitiesAsync(Guid countryId);

    Task<CityDto> GetCityByIdAsync(Guid cityId);

    Task AddCityAsync(AddCityRequest request);

    Task UpdateCityNameAsync(UpdateCityRequest request);

    Task DeleteCityAsync(Guid cityId);
}