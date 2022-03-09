using System;
using System.Threading.Tasks;
using AviaApp.Models;
using AviaApp.Services.Contracts;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AviaApp.Controllers;

public class AuthController : ControllerBase
{
    private readonly UserManager<AviaAppUser> _userManager;
    private readonly IAuthService _authService;

    public AuthController(UserManager<AviaAppUser> userManager, IAuthService authService)
    {
        _userManager = userManager;
        _authService = authService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return Ok(await _authService.LoginAsync(user));
        }

        return Unauthorized("Wrong email or password");
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var result = await _authService.RegisterAsync(model);

        return result.Status.Equals("Error", StringComparison.OrdinalIgnoreCase)
            ? BadRequest(result)
            : Ok(result);
    }
}