namespace CustomerCenter.Core.ResponseDTOs.Customer;

public class OrderItemDTO
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public double subtotal { get; set; }
    public OrderDTO Order { get; set; }
    public ProductionOrderDTO ProductionOrder { get; set; }
}