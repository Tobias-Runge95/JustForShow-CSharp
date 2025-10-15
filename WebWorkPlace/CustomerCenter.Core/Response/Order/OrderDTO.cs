using CustomerCenter.Database.Enums;

namespace CustomerCenter.Core.ResponseDTOs.Customer;

public class OrderDTO
{
    public Guid Id { get; set; }
    public CustomerDTO Customer { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public string ShippingAddress { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; }
}