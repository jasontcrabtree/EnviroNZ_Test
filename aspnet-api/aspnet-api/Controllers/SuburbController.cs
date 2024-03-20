using aspnet_api.Models;
using aspnet_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace SuburbSearch.Controllers;

[ApiController]
[Route("[api/controller]")]

public class SuburbController(SuburbService suburbService) : ControllerBase
{
    private readonly SuburbService _suburbService = suburbService;

    // Fetch all suburbs
    [HttpGet]
    public IEnumerable<Suburb>? Get()
    {
        return SuburbService.GetAllSuburbs();
    }

    // Search suburbs by ordered latitude/longitude
    [HttpGet("search")]
    public IActionResult GetClosestSuburb([FromQuery] double latitude, [FromQuery] double longitude)
    {
        var closestSuburb = _suburbService.FindClosestSuburb(latitude, longitude);
        if (closestSuburb == null)
        {
            return NotFound("No suburbs found.");
        }

        return Ok(closestSuburb);
    }


}
