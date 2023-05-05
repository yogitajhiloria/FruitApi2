using FruitApi.Bussiness.Interface;
using FruitApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace FruitApi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ErrorController : ControllerBase
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }


    [Route("/error")]
    public IActionResult HandleError() =>
    Problem();
}
