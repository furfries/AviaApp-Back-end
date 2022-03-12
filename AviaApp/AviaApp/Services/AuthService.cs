using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AviaApp.Models;
using AviaApp.Services.Contracts;
using Data.Entities;
using Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace AviaApp.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AviaAppUser> _userManager;

    public AuthService(UserManager<AviaAppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<LoginResponse> LoginAsync(AviaAppUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = new JwtSecurityToken(
            claims: authClaims
        );

        return new LoginResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Roles = userRoles,
        };
    }

    public async Task<AuthResponse> RegisterAsync(RegisterModel model)
    {
        var userExists = await _userManager.FindByNameAsync(model.Email);
        if (userExists != null)
        {
            return new AuthResponse { Status = "Error", Reasons = new List<string> { "User is already exists" }, };
        }

        var user = new AviaAppUser
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        user.UserName = user.Email;

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return new AuthResponse
            {
                Status = "Error",
                Reasons = result.Errors.Select(x => x.Description).ToList(),
            };
        }

        await _userManager.AddToRoleAsync(user, Role.User);

        return new AuthResponse { Status = "Success", Reasons = new List<string> { "User created successfully!" }, };
    }
}