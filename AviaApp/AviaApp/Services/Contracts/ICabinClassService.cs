using System.Collections.Generic;
using System.Threading.Tasks;
using AviaApp.Models.ViewModels;
using Data.Entities;

namespace AviaApp.Services.Contracts;

public interface ICabinClassService
{
    Task AddCabinClassesAsync(IList<CabinClass> cabinClasses);

    Task<CabinClass> GetByIdAsync(int id);

    Task<IList<CabinClassViewModel>> GetCabinClassesAsync();
}