using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AviaApp.Models.Dto;
using AviaApp.Models.Requests;

namespace AviaApp.Services.Contracts;

public interface IAirportService
{
    Task<IList<AirportDto>> GetAirportsAsync(Guid cityId);

    Task<AirportDto> GetAirportByIdAsync(Guid airportId);

    Task AddAirportAsync(AddAirportRequest request);

    Task UpdateAirportNameAsync(UpdateAirportRequest request);

    Task DeleteAirportAsync(Guid airportId);
}