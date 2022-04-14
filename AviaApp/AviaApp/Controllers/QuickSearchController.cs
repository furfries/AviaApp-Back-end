using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AviaApp.Models.ViewModels;
using AviaApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AviaApp.Controllers;

[ApiController]
[Route("[controller]")]
public class QuickSearchController : ControllerBase
{
    private readonly IQuickSearchService _quickSearchService;

    public QuickSearchController(IQuickSearchService quickSearchService)
    {
        _quickSearchService = quickSearchService;
    }

    /// <summary>
    /// Returns list of locations by search string
    /// </summary>
    /// <remarks>
    /// The endpoint returns the first five locations found
    /// </remarks>
    [HttpGet]
    [Route("/location")]
    [ProducesResponseType(typeof(List<LocationViewModel>), 200)]
    public async Task<IActionResult> GetFlightsByDateRangeAsync(string searchString)
    {
        try
        {
            if (string.IsNullOrEmpty(searchString) || string.IsNullOrWhiteSpace(searchString))
                return Ok(new List<LocationViewModel>());

            return Ok(await _quickSearchService.GetLocationsAsync(searchString));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}