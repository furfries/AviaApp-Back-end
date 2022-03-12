using System.Threading.Tasks;
using AviaApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AviaApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "admin,employee")]
    public async Task<IActionResult> GetUsersAsync()
    {
        return Ok(await _userService.GetUsersAsync());
    }
}