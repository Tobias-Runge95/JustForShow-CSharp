using MongoDB.Driver;
using ProductCenter.Database.Models;

namespace ProductCenter.Database;

public class ProductCenterDBContext
{
    public IMongoClient _client;
    public IMongoDatabase db;

    public ProductCenterDBContext()
    {
        _client = new MongoClient("mongodb://localhost:27013");
        db = _client.GetDatabase("ProductCenter");

        FoodProducts = db.GetCollection<FoodProduct>("FoodProducts");
        Products = db.GetCollection<Product>("Products");
    }

    public IMongoCollection<FoodProduct> FoodProducts { get; set; }
    public IMongoCollection<Product> Products { get; set; }
}