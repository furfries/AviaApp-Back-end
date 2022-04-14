using System.Collections.Generic;
using System.Threading.Tasks;
using AviaApp.Models.ViewModels;

namespace AviaApp.Services.Contracts;

public interface IQuickSearchService
{
    Task<IList<LocationViewModel>> GetLocationsAsync(string searchString);
}