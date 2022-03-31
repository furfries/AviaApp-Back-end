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
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
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
        return Ok(await _countryService.GetCountriesAsync());
    }

    /// <summary>
    /// Returns country by country Id
    /// </summary>
    /// <remarks>Endpoint available for authorized users</remarks>>
    /// <param name="countryId">Country Id</param>
    [HttpGet]
    [Route("{countryId:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(CountryDto), 200)]
    public async Task<IActionResult> GetCountryByIdAsync(Guid countryId)
    {
        try
        {
            return Ok(await _countryService.GetCountryByIdAsync(countryId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
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
            await _countryService.AddCountryAsync(countryName);
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
    public async Task<IActionResult> UpdateCountryNameAsync([FromBody] CountryDto updatedCountry)
    {
        try
        {
            await _countryService.UpdateCountryNameAsync(updatedCountry);
            return Ok("The country has been updated successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Deletes country
    /// </summary>
    /// <remarks>
    /// Endpoint available for "admin" and "employee" roles<br/>
    /// Related objects will also be deleted
    /// </remarks>>
    [HttpDelete("{countryId:guid}")]
    [Authorize(Roles = "admin,employee")]
    public async Task<IActionResult> DeleteCountryAsync(Guid countryId)
    {
        try
        {
            await _countryService.DeleteCountryAsync(countryId);
            return Ok("The country has been deleted");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}