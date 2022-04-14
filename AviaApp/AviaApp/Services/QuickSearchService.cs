using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AviaApp.Models.ViewModels;
using AviaApp.Services.Contracts;
using Data;
using Microsoft.EntityFrameworkCore;

namespace AviaApp.Services;

public class QuickSearchService : IQuickSearchService
{
    private readonly AviaAppDbContext _context;
    private readonly IMapper _mapper;
    private const int LocationsCount = 5;

    public QuickSearchService(AviaAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<LocationViewModel>> GetLocationsAsync(string searchString)
    {
        var locations = new List<LocationViewModel>();

        var airports = await _context.Airports
            .Include(x => x.City.Country)
            .Where(x => x.Name.Contains(searchString))
            .OrderBy(x => x.Name)
            .Take(LocationsCount)
            .ToListAsync();

        locations.AddRange(_mapper.Map<IList<LocationViewModel>>(airports));

        if (locations.Count == LocationsCount)
            return locations;

        var cities = await _context.Cities
            .Include(x => x.Country)
            .Where(x => x.Name.Contains(searchString))
            .OrderBy(x => x.Name)
            .Take(LocationsCount - locations.Count)
            .ToListAsync();

        locations.AddRange(_mapper.Map<IList<LocationViewModel>>(cities));

        if (locations.Count == LocationsCount)
            return locations;

        var countries = await _context.Countries
            .Where(x => x.Name.Contains(searchString))
            .OrderBy(x => x.Name)
            .Take(LocationsCount - locations.Count)
            .ToListAsync();

        locations.AddRange(_mapper.Map<IList<LocationViewModel>>(countries));

        return locations;
    }
}