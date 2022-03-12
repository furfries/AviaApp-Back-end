using System.Collections.Generic;
using System.Threading.Tasks;
using AviaApp.Models;

namespace AviaApp.Services.Contracts;

public interface IUserService
{
    public Task<IList<UserDto>> GetUsersAsync();

    public Task<UpdateRoleResponse> AddRoleAsync(UpdateRoleModel model);

    public Task<UpdateRoleResponse> DeleteRoleAsync(UpdateRoleModel model);
}