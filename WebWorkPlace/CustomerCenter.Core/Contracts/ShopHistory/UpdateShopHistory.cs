using CustomerCenter.Database.Models;

namespace CustomerCenter.Core.Contracts.ShopHistory;

public class UpdateShopHistory
{
    public Guid CustomerId { get; set; }
    public Basket Basket { get; set; }
}