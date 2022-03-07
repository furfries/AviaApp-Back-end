using System.Threading.Tasks;
using AviaApp.Models;
using Data.Entities;

namespace AviaApp.Services.Contracts;

public interface IAuthService
{
    public Task<LoginResponse> LoginAsync(AviaAppUser user);

    public Task<AuthResponse> RegisterAsync(RegisterModel model);
}