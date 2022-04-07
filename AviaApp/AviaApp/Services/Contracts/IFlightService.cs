using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AviaApp.Models.Dto;

namespace AviaApp.Services.Contracts;

public interface IFlightService
{
    Task<IList<FlightDto>> GetFlightsAsync();

    Task<FlightDto> GetFlightByIdAsync(Guid flightId);
}