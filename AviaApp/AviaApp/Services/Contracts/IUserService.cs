using System.Collections.Generic;
using System.Threading.Tasks;
using AviaApp.Models;

namespace AviaApp.Services.Contracts;

public interface IUserService
{
    public Task<IList<UserDto>> GetUsersAsync();
}