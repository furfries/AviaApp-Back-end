using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AviaApp.Models;
using AviaApp.Services.Contracts;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AviaApp.Services;

public class UserService : IUserService
{
    private readonly UserManager<AviaAppUser> _userManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<AviaAppUser> userManager,
        IMapper mapper
    )
    {
        _userManager = userManager;
        _mapper = mapper;
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
}