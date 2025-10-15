using CustomerCenter.Database.Enums;

namespace CustomerCenter.Database.Models;

public class ProductionOrder
{
    public Guid Id { get; set; }
    public Guid OrderItemId { get; set; }
    public OrderItem OrderItem { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int ProducedQuantity { get; set; }
    public ProductionOrderStatus Status { get; set; }
}