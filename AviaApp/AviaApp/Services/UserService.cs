using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AviaApp.Enums;
using AviaApp.Models;
using AviaApp.Models.Dto;
using AviaApp.Services.Contracts;
using Data.Entities;
using Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AviaApp.Services;

public class UserService : IUserService
{
    private readonly UserManager<AviaAppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<AviaAppUser> userManager,
        IMapper mapper,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _roleManager = roleManager;
    }

    public async Task<IList<UserDto>> GetUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();

        var userDtos = new List<UserDto>();
        foreach (var user in users)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Roles = userRoles;
            userDtos.Add(userDto);
        }

        return userDtos;
    }

    public async Task<UpdateRoleResponse> AddRoleAsync(UpdateRoleModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        var userRoles = await _userManager.GetRolesAsync(user);

        var role = await _roleManager.FindByNameAsync(model.Role);
        if (role == null)
            return new UpdateRoleResponse
                {Status = Status.Error, Message = $"The role \'{model.Role}\' does not exist"};

        if (userRoles.Contains(model.Role))
            return new UpdateRoleResponse
                {Status = Status.Error, Message = $"The user \'{model.Email}\' already has this role"};

        if (model.Role == Role.Banned)
        {
            await AssignRoleAsync(user, userRoles, Role.Banned);
            return new UpdateRoleResponse
                {Status = Status.Success, Message = $"User \'{model.Email}\' has been banned"};
        }

        if (model.Role == Role.User)
        {
            await AssignRoleAsync(user, userRoles, Role.User);
            return new UpdateRoleResponse
                {Status = Status.Success, Message = $"User \'{model.Email}\' has been unbanned"};
        }

        if (model.Role == Role.Admin && !userRoles.Contains(Role.Employee))
            return new UpdateRoleResponse
                {Status = Status.Error, Message = $"The user should have \'{Role.Employee}\' role"};

        if (model.Role == Role.Employee && !userRoles.Contains(Role.User))
            return new UpdateRoleResponse
                {Status = Status.Error, Message = $"The user should have \'{Role.User}\' role"};

        await _userManager.AddToRoleAsync(user, model.Role);
        return new UpdateRoleResponse
            {Status = Status.Success, Message = $"The role \'{model.Role}\' has been added successfully!"};
    }

    public async Task<UpdateRoleResponse> DeleteRoleAsync(UpdateRoleModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        var userRoles = await _userManager.GetRolesAsync(user);

        var role = await _roleManager.FindByNameAsync(model.Role);
        if (role == null)
            return new UpdateRoleResponse
                {Status = Status.Error, Message = $"The role \'{model.Role}\' does not exist"};

        if (!userRoles.Contains(model.Role))
            return new UpdateRoleResponse
                {Status = Status.Error, Message = $"The user does not have \'{model.Role}\' role"};

        if (model.Role == Role.Banned || model.Role == Role.User)
            return new UpdateRoleResponse
                {Status = Status.Error, Message = $"It is not possible to delete \'{model.Role}\' role"};

        if (model.Role == Role.Employee && userRoles.Contains(Role.Admin))
            return new UpdateRoleResponse
                {Status = Status.Error, Message = $"Firstly need to delete \'{Role.Admin}\' role"};

        await _userManager.RemoveFromRoleAsync(user, model.Role);
        return new UpdateRoleResponse
            {Status = Status.Success, Message = $"The role \'{model.Role}\' has been deleted successfully"};
    }

    private async Task AssignRoleAsync(AviaAppUser user, IEnumerable<string> roles, string role)
    {
        await _userManager.RemoveFromRolesAsync(user, roles);
        await _userManager.AddToRoleAsync(user, role);
    }
}