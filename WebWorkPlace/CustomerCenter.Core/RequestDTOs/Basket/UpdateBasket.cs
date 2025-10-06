namespace CustomerCenter.Core.RequestDTOs.Basket;

public class UpdateBasket
{
    public Guid BasketId { get; set; }
    public List<object> Items { get; set; }
    public DateOnly? BoughtAt { get; set; }
}