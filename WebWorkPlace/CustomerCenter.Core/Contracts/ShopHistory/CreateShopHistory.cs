using CustomerCenter.Database.Models;

namespace CustomerCenter.Core.Contracts.ShopHistory;

public class CreateShopHistory
{
    public Guid CustomerId { get; set; }
    public Basket Basket { get; set; }
}