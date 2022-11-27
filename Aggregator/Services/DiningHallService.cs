using System.Collections.Concurrent;
using Aggregator.Controllers;
using Aggregator.Models;
using Microsoft.Extensions.Options;

namespace Aggregator.Services;

public class DiningHallService : BackgroundService
{
    private readonly AggregatorOptions config;
    private readonly IOrderService orderService;
    private readonly ILogger<DiningHallService> logger;
    private readonly ConcurrentQueue<Order> orderQueue;

    public DiningHallService(
        IOrderService orderService,
        IOptions<AggregatorOptions> options,
        ILogger<DiningHallService> logger)
    {
        this.config = options.Value;
        this.orderService = orderService;
        this.logger = logger;

        this.orderQueue = new ConcurrentQueue<Order>();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        for (int i = 0; i < this.config.DiningHallThreads; i++)
        {
            Task.Run(ProcessQueue, stoppingToken);
        }
        return Task.CompletedTask;
    }

    private async Task ProcessQueue()
    {
        while (true)
        {
            if (!this.orderQueue.TryDequeue(out var order))
                continue;

            this.logger.LogInformation($"D FORWARDING ORDER {order.Id}");

            await this.orderService.PostAsync("https://localhost:7226/order", order);
            // var response = await httpClient.PostAsJsonAsync("/order", order);
            // this.logger.LogInformation($"D RESPONSE {response}");
        }
    }

    public void Enqueue(Order order)
    {
        this.orderQueue.Enqueue(order);
    }
}
