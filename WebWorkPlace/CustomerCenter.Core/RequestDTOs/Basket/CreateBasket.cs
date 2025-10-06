namespace CustomerCenter.Core.RequestDTOs.Basket;

public class CreateBasket
{
    public Guid CustomerId { get; set; }
    // TODO: Add real Item object from ProductCenter
    public List<object> Items { get; set; }
    public DateOnly? BoughtAt { get; set; }
}