using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AviaApp.Models.ViewModels;
using AviaApp.Services.Contracts;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AviaApp.Services;

public class CabinClassService : ICabinClassService
{
    private readonly AviaAppDbContext _context;
    private readonly IMapper _mapper;

    public CabinClassService(AviaAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AddCabinClassesAsync(IList<CabinClass> cabinClasses)
    {
        if (!_context.CabinClasses.Any())
        {
            await _context.CabinClasses.AddRangeAsync(cabinClasses);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<CabinClass> GetByIdAsync(int id)
    {
        return await _context.CabinClasses.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IList<CabinClassViewModel>> GetCabinClassesAsync()
    {
        return _mapper.Map<IList<CabinClassViewModel>>(await _context.CabinClasses.ToListAsync());
    }
}