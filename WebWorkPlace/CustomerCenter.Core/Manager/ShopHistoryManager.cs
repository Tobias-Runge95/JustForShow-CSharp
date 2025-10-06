using CustomerCenter.Core.Contracts.ShopHistory;
using CustomerCenter.Core.CustomerCenterService;
using CustomerCenter.Core.RequestDTOs.ShopHistory;
using CustomerCenter.Core.ResponseDTOs.Basket;
using CustomerCenter.Core.ResponseDTOs.ShopHistory;
using CustomerCenter.Database;
using CustomerCenter.Database.Models;
using MongoDB.Driver;

namespace CustomerCenter.Core.Manager;

public interface IShopHistoryManager
{
    Task CrateShopHistory(CreateShopHistory createShopHistory);
    Task UpdateShopHistory(UpdateShopHistory updateShopHistory);
    Task DeleteShopHistory(DeleteShopHistory deleteShopHistory);
    Task<ShopHistoryDTO> GetShopHistory(GetShopHistory getShopHistory);
}

public class ShopHistoryManagerManager : IShopHistoryManager
{
    private readonly CustomerCenterDbContext _customerCenterDbContext;

    public ShopHistoryManagerManager(CustomerCenterDbContextFactory customerCenterDbContextFactory)
    {
        _customerCenterDbContext = customerCenterDbContextFactory.CreateCustomerCenterDbContext();
    }
    
    public async Task CrateShopHistory(CreateShopHistory createShopHistory)
    {
        await _customerCenterDbContext.ShopHistory.InsertOneAsync(new ShopHistory
        {
            Id = Guid.NewGuid(),
            CustomerId = createShopHistory.CustomerId,
            Baskets = new List<Basket>(),
            CreatedAt = DateTime.Now
        });
    }

    public async Task UpdateShopHistory(UpdateShopHistory updateShopHistory)
    {
        var filter = Builders<ShopHistory>.Filter.Eq(x => x.CustomerId, updateShopHistory.CustomerId);
        var shopHistory = await _customerCenterDbContext.ShopHistory.Find(filter).FirstAsync();
        shopHistory.UpdatedAt = DateTime.Now;
        shopHistory.Baskets.Add(updateShopHistory.Basket);

        await _customerCenterDbContext.ShopHistory.ReplaceOneAsync(filter, shopHistory);
    }

    public async Task DeleteShopHistory(DeleteShopHistory deleteShopHistory)
    {
        var filter = Builders<ShopHistory>.Filter.Eq(x => x.CustomerId, deleteShopHistory.CustomerId);
        await _customerCenterDbContext.ShopHistory.DeleteOneAsync(filter);
    }

    public async Task<ShopHistoryDTO> GetShopHistory(GetShopHistory getShopHistory)
    {
        var filter = Builders<ShopHistory>.Filter.Eq(x => x.CustomerId, getShopHistory.CustomerId);
        var shopHistory = await _customerCenterDbContext.ShopHistory.Find(filter).FirstAsync();

        return new ShopHistoryDTO
        {
            Baskets = shopHistory.Baskets.Select(x =>
            {
                return new BasketDTO
                {
                    BoughtAt = x.BoughtAt,
                    Items = x.Items,
                    BasketId = x.Id
                };
            }).ToList()
        };
    }
}