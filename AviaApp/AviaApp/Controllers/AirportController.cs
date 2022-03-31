using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AviaApp.Models.Dto;
using AviaApp.Models.Requests;
using AviaApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AviaApp.Controllers;

[ApiController]
[Route("[controller]")]
public class AirportController : ControllerBase
{
    
    private readonly IAirportService _airportService;

    public AirportController(IAirportService airportService)
    {
        _airportService = airportService;
    }

    /// <summary>
    /// Returns list of airports by city Id
    /// </summary>
    /// <remarks>Endpoint available for authorized users</remarks>>
    /// <param name="cityId">City Id</param>
    [HttpGet]
    [Route("list/{cityId:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(List<AirportDto>), 200)]
    public async Task<IActionResult> GetAirportsAsync(Guid cityId)
    {
        return Ok(await _airportService.GetAirportsAsync(cityId));
    }

    /// <summary>
    /// Returns airport by airport Id
    /// </summary>
    /// <remarks>Endpoint available for authorized users</remarks>>
    /// <param name="airportId">Airport Id</param>
    [HttpGet]
    [Route("{airportId:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(AirportDto), 200)]
    public async Task<IActionResult> GetAirportByIdAsync(Guid airportId)
    {
        try
        {
            return Ok(await _airportService.GetAirportByIdAsync(airportId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Adds airport
    /// </summary>
    /// <remarks>
    /// Endpoint available for "admin" and "employee" roles<br/>
    /// The field "name" is unique for city group
    /// </remarks>>
    [HttpPost]
    [Authorize(Roles = "admin,employee")]
    public async Task<IActionResult> AddAirportAsync(AddAirportRequest request)
    {
        try
        {
            await _airportService.AddAirportAsync(request);
            return Ok($"The airport {request.AirportName} has been added successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Updates airport name
    /// </summary>
    /// <remarks>
    /// Endpoint available for "admin" and "employee" roles<br/>
    /// The field "name" is unique for city group
    /// </remarks>>
    [HttpPut]
    [Authorize(Roles = "admin,employee")]
    public async Task<IActionResult> UpdateAirportNameAsync([FromBody] UpdateAirportRequest request)
    {
        try
        {
            await _airportService.UpdateAirportNameAsync(request);
            return Ok("The airport has been updated successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Deletes airport
    /// </summary>
    /// <remarks>
    /// Endpoint available for "admin" and "employee" roles<br/>
    /// </remarks>>
    [HttpDelete("{airportId:guid}")]
    [Authorize(Roles = "admin,employee")]
    public async Task<IActionResult> DeleteAirportAsync(Guid airportId)
    {
        try
        {
            await _airportService.DeleteAirportAsync(airportId);
            return Ok("The airport has been deleted");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}