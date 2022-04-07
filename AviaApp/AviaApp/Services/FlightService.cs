using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AviaApp.Models.Dto;
using AviaApp.Services.Contracts;
using Data;
using Microsoft.EntityFrameworkCore;

namespace AviaApp.Services;

public class FlightService : IFlightService
{
    private readonly IMapper _mapper;
    private readonly AviaAppDbContext _context;
    private readonly ICountryService _countryService;

    public FlightService(IMapper mapper, AviaAppDbContext context, ICountryService countryService)
    {
        _mapper = mapper;
        _context = context;
        _countryService = countryService;
    }

    public async Task<IList<FlightDto>> GetFlightsAsync()
    {
        var flights = await _context.Flights
            .Include(x => x.AirportFrom.City)
            .Include(x => x.AirportTo.City)
            .Where(x => x.FlightDateTime > DateTime.Now)
            .ToListAsync();

        var flightDtos = _mapper.Map<IList<FlightDto>>(flights);
        foreach (var flightDto in flightDtos)
        {
            await AssignCountriesForAirportsAsync(flightDto);
        }

        return flightDtos;
    }

    public async Task<FlightDto> GetFlightByIdAsync(Guid flightId)
    {
        var flight = await _context.Flights
            .Include(x => x.AirportFrom.City)
            .Include(x => x.AirportTo.City)
            .FirstOrDefaultAsync(x => x.Id == flightId);

        if (flight == null)
            throw new Exception("The flight has not been found");

        var flightDto = _mapper.Map<FlightDto>(flight);
        await AssignCountriesForAirportsAsync(flightDto);

        return flightDto;
    }

    public async Task<FlightDto> UpdateFlightAsync(Guid flightId)
    {
        var flight = await _context.Flights
            .Include(x => x.AirportFrom.City).Include(x => x.AirportTo.City)
            .FirstOrDefaultAsync(x => x.Id == flightId);

        if (flight == null)
            throw new Exception("The flight has not been found");

        var flightDto = _mapper.Map<FlightDto>(flight);
        await AssignCountriesForAirportsAsync(flightDto);

        return flightDto;
    }


    private async Task AssignCountriesForAirportsAsync(FlightDto flight)
    {
        await _countryService.AssignCountryAsync(flight.AirportFrom);
        await _countryService.AssignCountryAsync(flight.AirportTo);
    }
}