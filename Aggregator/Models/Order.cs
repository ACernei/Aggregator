namespace Aggregator.Models;

public class Order
{
    public int Id { get; set; }
    public List<int> Items { get; set; }
    public int Priority { get; set; }
    public int MaxWait { get; set; }
}
