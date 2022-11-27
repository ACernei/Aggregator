using Aggregator.Models;

namespace Aggregator.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient httpClient;
    private readonly ILogger<OrderService> logger;

    public OrderService(HttpClient httpClient, ILogger<OrderService> logger)
    {
        this.httpClient = httpClient;
        this.logger = logger;
    }

    public async Task PostAsync(string uri, Order order)
    {
        using var response = await httpClient.PostAsJsonAsync(uri, order);
        this.logger.LogInformation(response.ToString());
    }
}
