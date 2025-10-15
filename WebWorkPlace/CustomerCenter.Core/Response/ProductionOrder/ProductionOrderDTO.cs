using CustomerCenter.Database.Enums;

namespace CustomerCenter.Core.ResponseDTOs.Customer;

public class ProductionOrderDTO
{
    public Guid Id { get; set; }
    public OrderItemDTO OrderItem { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int ProducedQuantity { get; set; }
    public ProductionOrderStatus Status { get; set; }
}