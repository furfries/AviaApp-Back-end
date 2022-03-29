using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AviaApp.Models.Dto;
using AviaApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AviaApp.Controllers;

[ApiController]
[Route("[controller]")]
public class CountryController : ControllerBase
{
    private readonly ILocationService _locationService;

    public CountryController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    /// <summary>
    /// Returns list of countries
    /// </summary>
    /// <remarks>Endpoint available for authorized users</remarks>>
    [HttpGet]
    [Route("list")]
    [Authorize]
    [ProducesResponseType(typeof(List<CountryDto>), 200)]
    public async Task<IActionResult> GetCountriesAsync()
    {
        return Ok(await _locationService.GetCountriesAsync());
    }

    /// <summary>
    /// Returns country by country Id
    /// </summary>
    /// <remarks>Endpoint available for authorized users</remarks>>
    /// <param name="id">Country Id</param>
    [HttpGet]
    [Route("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(CountryDto), 200)]
    public async Task<IActionResult> GetCountryByIdAsync(Guid id)
    {
        var country = await _locationService.GetCountryByIdAsync(id);
        if (country == null)
        {
            return StatusCode(404, "The country has not been found");
        }

        return Ok(country);
    }

    /// <summary>
    /// Adds country
    /// </summary>
    /// <remarks>
    /// Endpoint available for "admin" and "employee" roles<br/>
    /// The field "name" is unique
    /// </remarks>>
    [HttpPost]
    [Authorize(Roles = "admin,employee")]
    public async Task<IActionResult> AddCountryAsync(string countryName)
    {
        try
        {
            await _locationService.AddCountryAsync(countryName);
            return Ok($"The country {countryName} has been added successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Updates country name
    /// </summary>
    /// <remarks>
    /// Endpoint available for "admin" and "employee" roles<br/>
    /// The field "name" is unique
    /// </remarks>>
    [HttpPut]
    [Authorize(Roles = "admin,employee")]
    public async Task<IActionResult> UpdateCountryNameAsync([FromBody] CountryDto country)
    {
        try
        {
            await _locationService.UpdateCountryNameAsync(country);
            return Ok("The country has been updated successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}