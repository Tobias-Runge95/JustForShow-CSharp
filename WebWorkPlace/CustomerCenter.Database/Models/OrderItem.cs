namespace CustomerCenter.Database.Models;

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public double subtotal { get; set; }
    public Order Order { get; set; }
    public ProductionOrder ProductionOrder { get; set; }
}