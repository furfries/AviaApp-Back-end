using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AviaApp.Models;
using AviaApp.Services.Contracts;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AviaApp.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AviaAppUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<AviaAppUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
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

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new LoginResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ValidTo = token.ValidTo,
        };
    }

    public async Task<AuthResponse> RegisterAsync(RegisterModel model)
    {
        var userExists = await _userManager.FindByNameAsync(model.Email);
        if (userExists != null)
        {
            return new AuthResponse { Status = "Error", Message = "User already exists!" };
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
                Message = "User creation failed! Please check user details and try again."
            };
        }

        return new AuthResponse { Status = "Success", Message = "User created successfully!" };
    }
}