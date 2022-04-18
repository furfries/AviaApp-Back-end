using System.Collections.Generic;
using System.Threading.Tasks;
using AviaApp.Models.ViewModels;
using AviaApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AviaApp.Controllers;

[ApiController]
[Microsoft.AspNetCore.Components.Route("[controller]")]
public class CabinClassController : ControllerBase
{
    private readonly ICabinClassService _cabinClassService;

    public CabinClassController(ICabinClassService cabinClassService)
    {
        _cabinClassService = cabinClassService;
    }

    /// <summary>
    /// Returns list of cabin classes
    /// </summary>
    [HttpGet]
    [Route("list")]
    [ProducesResponseType(typeof(List<CabinClassViewModel>), 200)]
    public async Task<IActionResult> GetCabinClassesAsync()
    {
        return Ok(await _cabinClassService.GetCabinClassesAsync());
    }
}