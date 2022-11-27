using System.Collections.Concurrent;
using Aggregator.Models;
using Aggregator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Aggregator.Controllers;

[ApiController]
[Route("order")]
public class DiningHallController : ControllerBase
{
    private readonly DiningHallService diningHallService;
    private readonly ILogger<DiningHallController> logger;


    public DiningHallController(
        DiningHallService diningHallService,
        ILogger<DiningHallController> logger)
    {
        this.diningHallService = diningHallService;
        this.logger = logger;
    }

    [HttpPost]
    public IActionResult Post(Order order)
    {
        this.logger.LogInformation($"D RECEIVED ORDER {order.Id}");

        this.diningHallService.Enqueue(order);
        return Ok();
    }
}
