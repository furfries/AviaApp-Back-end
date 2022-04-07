using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AviaApp.Models.Dto;
using AviaApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AviaApp.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController : ControllerBase
{
    private readonly IFlightService _flightService;

    public FlightController(IFlightService flightService)
    {
        _flightService = flightService;
    }

    /// <summary>
    /// Returns list of flights
    /// </summary>
    [HttpGet]
    [Route("list")]
    [ProducesResponseType(typeof(List<FlightDto>), 200)]
    public async Task<IActionResult> GetFlightsAsync()
    {
        try
        {
            return Ok(await _flightService.GetFlightsAsync());
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Returns flight by Id
    /// </summary>
    [HttpGet]
    [Route("{flightId:guid}")]
    [ProducesResponseType(typeof(FlightDto), 200)]
    public async Task<IActionResult> GetFlightByIdAsync(Guid flightId)
    {
        try
        {
            return Ok(await _flightService.GetFlightByIdAsync(flightId));
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}