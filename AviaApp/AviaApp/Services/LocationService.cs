using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AviaApp.Models.Dto;
using AviaApp.Services.Contracts;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AviaApp.Services;

public class LocationService : ILocationService
{
    private readonly AviaAppDbContext _context;
    private readonly IMapper _mapper;

    public LocationService(AviaAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<CountryDto>> GetCountriesAsync()
    {
        return _mapper.Map<IList<CountryDto>>(await _context.Countries.ToListAsync());
    }

    public async Task<CountryDto> GetCountryByIdAsync(Guid id)
    {
        return _mapper.Map<CountryDto>(await _context.Countries.FirstOrDefaultAsync(x => x.Id == id));
    }

    public async Task AddCountryAsync(string countryName)
    {
        await CheckCountryName(countryName);

        var country = new Country
        {
            Id = new Guid(),
            Name = countryName,
        };
        await _context.AddAsync(country);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCountryNameAsync(CountryDto country)
    {
        var existingCountry = await _context.Countries.FirstOrDefaultAsync(x => x.Id == country.Id);
        if (existingCountry == null)
            throw new Exception($"This country does not exist");

        await CheckCountryName(country.Name);
        existingCountry.Name = country.Name;

        _context.Countries.Update(existingCountry);
        await _context.SaveChangesAsync();
    }

    private async Task CheckCountryName(string countryName)
    {
        var isCountryExist = await _context.Countries.AnyAsync(x => x.Name == countryName);
        if (isCountryExist)
            throw new Exception($"The country {countryName} already exists");
    }
}