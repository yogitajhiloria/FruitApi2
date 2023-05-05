using FruitApi.Bussiness.Interface;
using FruitApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace FruitApi.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class FruitController : ControllerBase
{
    private readonly ILogger<FruitController> _logger;
    private readonly IFruityviceService fruityviceService;

    public FruitController(ILogger<FruitController> logger, IFruityviceService fruityviceService)
    {
        _logger = logger;
        this.fruityviceService = fruityviceService;
    }

    [HttpGet(Name = "GetAllFruits")]
    public async Task<IEnumerable<Fruits>> Get(CancellationToken cancellationToken)
    {
        return await this.fruityviceService.GetAllFruits(cancellationToken);
    }

    [HttpPost(Name = "GetFruitForFamily")]
    public async Task<IEnumerable<Fruits>> Post([FromBody]FruitFilter fruitFilter, CancellationToken cancellationToken)
    {
        return await this.fruityviceService.RetriveAllFruitsForFamnily(fruitFilter.FruitFamily,cancellationToken);
    }
}
