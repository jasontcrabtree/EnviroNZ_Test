using Microsoft.AspNetCore.Mvc;

namespace Suburb.Controllers;

[ApiController]
[Route("[suburb]")]

public class SuburbController : ControllerBase
{
    private readonly ILogger<SuburbController> _logger;

    public SuburbController(ILogger<SuburbController> logger)
    {
        _logger = logger;
    }


    [HttpGet]

}
