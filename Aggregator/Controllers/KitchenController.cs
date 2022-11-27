using System.Collections.Concurrent;
using Aggregator.Models;
using Aggregator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Aggregator.Controllers;

[ApiController]
[Route("distribution")]
public class KitchenController : ControllerBase
{
    private readonly ILogger<KitchenController> logger;
    private readonly KitchenService kitchenService;

    public KitchenController(
        KitchenService kitchenService,
        ILogger<KitchenController> logger)
    {
        this.kitchenService = kitchenService;
        this.logger = logger;
    }

    [HttpPost]
    public IActionResult Post(Order order)
    {
        this.logger.LogInformation($"K RECEIVED ORDER {order.Id}");

        this.kitchenService.Enqueue(order);
        return Ok();
    }
}
