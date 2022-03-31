using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AviaApp.Models.Dto;
using AviaApp.Models.Requests;
using AviaApp.Services.Contracts;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AviaApp.Services;

public class CityService : ICityService
{
    private readonly AviaAppDbContext _context;
    private readonly IMapper _mapper;

    public CityService(AviaAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<CityDto>> GetCitiesAsync(Guid countryId)
    {
        return _mapper.Map<IList<CityDto>>(await _context.Cities.Where(x => x.CountryId == countryId)
            .Include(x => x.Country).ToListAsync());
    }

    public async Task<CityDto> GetCityByIdAsync(Guid cityId)
    {
        var city = await GetCityIfExistsAsync(cityId);
        return _mapper.Map<CityDto>(city);
    }

    public async Task AddCityAsync(AddCityRequest request)
    {
        await CheckCityName(request.CityName, request.CountryId);

        var city = new City
        {
            Id = new Guid(),
            Name = request.CityName,
            CountryId = request.CountryId,
        };
        await _context.Cities.AddAsync(city);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCityNameAsync(UpdateCityRequest request)
    {
        var existingCity = await GetCityIfExistsAsync(request.CityId);
        await CheckCityName(request.UpdatedCityName, existingCity.Country.Id);

        existingCity.Name = request.UpdatedCityName;

        _context.Cities.Update(existingCity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCityAsync(Guid cityId)
    {
        var city = await GetCityIfExistsAsync(cityId);
        _context.Cities.Remove(city);
        await _context.SaveChangesAsync();
    }

    private async Task<City> GetCityIfExistsAsync(Guid cityId)
    {
        var city = await _context.Cities.Include(x => x.Country).FirstOrDefaultAsync(x => x.Id == cityId);
        if (city == null)
            throw new Exception("This city has not been found");

        return city;
    }

    private async Task CheckCityName(string cityName, Guid countryId)
    {
        var city = await _context.Cities.FirstOrDefaultAsync(x =>
            x.Name == cityName && x.Country.Id == countryId);

        if (city != null)
            throw new Exception($"The city {cityName} already exists");
    }
}