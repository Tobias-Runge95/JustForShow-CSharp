using CustomerCenter.Core.ResponseDTOs.Basket;

namespace CustomerCenter.Core.ResponseDTOs.ShopHistory;

public class ShopHistoryDTO
{
    public List<BasketDTO> Baskets { get; set; }
}