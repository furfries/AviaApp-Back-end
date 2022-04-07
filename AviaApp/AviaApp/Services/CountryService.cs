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

public class CountryService : ICountryService
{
    private readonly AviaAppDbContext _context;
    private readonly IMapper _mapper;

    public CountryService(AviaAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<CountryDto>> GetCountriesAsync()
    {
        return _mapper.Map<IList<CountryDto>>(await _context.Countries.ToListAsync());
    }

    public async Task<CountryDto> GetCountryByIdAsync(Guid countryId)
    {
        var country = await GetCountryIfExistsAsync(countryId);
        return _mapper.Map<CountryDto>(country);
    }

    public async Task AddCountryAsync(string countryName)
    {
        await CheckCountryName(countryName);

        var country = new Country
        {
            Id = new Guid(),
            Name = countryName,
        };
        await _context.Countries.AddAsync(country);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCountryNameAsync(CountryDto updatedCountry)
    {
        var existingCountry = await GetCountryIfExistsAsync(updatedCountry.Id);
        await CheckCountryName(updatedCountry.Name);

        existingCountry.Name = updatedCountry.Name;

        _context.Countries.Update(existingCountry);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCountryAsync(Guid countryId)
    {
        var country = await GetCountryIfExistsAsync(countryId);
        _context.Countries.Remove(country);
        await _context.SaveChangesAsync();
    }

    public async Task AssignCountryAsync(AirportDto airport)
    {
        var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == airport.City.CountryId);
        airport.City.Country = _mapper.Map<CountryDto>(country);
    }

    private async Task CheckCountryName(string countryName)
    {
        var isCountryExist = await _context.Countries.AnyAsync(x => x.Name == countryName);
        if (isCountryExist)
            throw new Exception($"The country {countryName} already exists");
    }

    private async Task<Country> GetCountryIfExistsAsync(Guid countryId)
    {
        var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == countryId);
        if (country == null)
            throw new Exception("This country has not been found");

        return country;
    }
}