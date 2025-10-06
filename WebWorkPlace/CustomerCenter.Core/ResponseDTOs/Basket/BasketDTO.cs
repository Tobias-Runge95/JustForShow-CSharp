namespace CustomerCenter.Core.ResponseDTOs.Basket;

public class BasketDTO
{
    public Guid BasketId { get; set; }
    public List<object> Items { get; set; }
    public DateOnly? BoughtAt { get; set; }
}