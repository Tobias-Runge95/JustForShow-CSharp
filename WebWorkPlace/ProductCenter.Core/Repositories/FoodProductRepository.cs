using MongoDB.Driver;
using ProductCenter.Database;
using ProductCenter.Database.Models;

namespace ProductCenter.Core.Repositories;

public class FoodProductRepository
{
    public IMongoCollection<FoodProduct> _foodProductCollection { get; set; }

    public FoodProductRepository(ProductCenterDBContext productCenterDbContext)
    {
        _foodProductCollection = productCenterDbContext.FoodProducts;
    }

    private IQueryable<FoodProduct> Get() => _foodProductCollection.AsQueryable();

    public FoodProduct GetFoodProduct(Guid id) => Get().FirstOrDefault(x => x.Id == id);

    public List<FoodProduct> GetAllFoodProducts() => Get().ToList();

    public void CreateFoodProduct(FoodProduct foodProduct)
    {
        _foodProductCollection.InsertOne(foodProduct);
    }
}