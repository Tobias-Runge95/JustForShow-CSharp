using CustomerCenter.Core.CustomerCenterService;
using CustomerCenter.Core.RequestDTOs.Basket;
using CustomerCenter.Core.ResponseDTOs.Basket;
using CustomerCenter.Database;
using CustomerCenter.Database.Models;
using MongoDB.Driver;

namespace CustomerCenter.Core.Manager;

public interface IBasketManager
{
    Task CreateBasket(CreateBasket createBasket);
    Task UpdateBasket(UpdateBasket updateBasket);
    Task DeleteBasket(DeleteBasket deleteBasket);
    Task<BasketDTO> GetBasket(GetBasket getBasket);
}

public class BasketManager : IBasketManager
{
    private readonly CustomerCenterDbContext _customerCenterDbContext;

    public BasketManager(CustomerCenterDbContextFactory customerCenterDbContextFactory)
    {
        _customerCenterDbContext = customerCenterDbContextFactory.CreateCustomerCenterDbContext();
    }
    
    public async Task CreateBasket(CreateBasket createBasket)
    {
        await _customerCenterDbContext.Basket.InsertOneAsync(new Basket
        {
            Id = Guid.NewGuid(),
            CustomerId = createBasket.CustomerId,
            BoughtAt = createBasket.BoughtAt,
            Items = createBasket.Items,
            CreatedAt = DateTime.Now
        });
    }

    public async Task UpdateBasket(UpdateBasket updateBasket)
    {
        var filter = Builders<Basket>.Filter.Eq(x => x.Id, updateBasket.BasketId);
        var updateDefinition = Builders<Basket>.Update.Combine(
            Builders<Basket>.Update.Set(x => x.BoughtAt, updateBasket.BoughtAt),
            Builders<Basket>.Update.Set(x => x.Items, updateBasket.Items));

        await _customerCenterDbContext.Basket.UpdateOneAsync(filter, updateDefinition);
    }

    public async Task DeleteBasket(DeleteBasket deleteBasket)
    {
        var filter = Builders<Basket>.Filter.Eq(x => x.Id, deleteBasket.BasketId);
        await _customerCenterDbContext.Basket.DeleteOneAsync(filter);
    }

    public async Task<BasketDTO> GetBasket(GetBasket getBasket)
    {
        var filter = Builders<Basket>.Filter.Eq(x => x.Id, getBasket.BasketId);
        var basket = await _customerCenterDbContext.Basket.Find(filter).FirstAsync();

        return new BasketDTO
        {
            BoughtAt = basket.BoughtAt,
            BasketId = basket.Id,
            Items = basket.Items
        };
    }
}