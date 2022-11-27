using Aggregator.Models;

namespace Aggregator.Services;

public interface IOrderService
{
    public Task PostAsync(string uri, Order order);
}
