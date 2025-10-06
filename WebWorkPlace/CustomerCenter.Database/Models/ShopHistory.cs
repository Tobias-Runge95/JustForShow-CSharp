namespace CustomerCenter.Database.Models;

public class ShopHistory : BaseCustomerModelClass
{
    public Guid CustomerId { get; set; }
    public List<Basket> Baskets { get; set; }
    public DateTime? UpdatedAt { get; set; }
}