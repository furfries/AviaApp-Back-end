using System.Threading.Tasks;
using AviaApp.Models.Requests;

namespace AviaApp.Services.Contracts;

public interface IBookingService
{
    Task BookFlightAsync(BookFlightRequest request);
}