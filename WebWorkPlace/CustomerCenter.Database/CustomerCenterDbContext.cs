using CustomerCenter.Database.Models;
using MongoDB.Driver;

namespace CustomerCenter.Database;

public class CustomerCenterDbContext
{
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _db;

    public CustomerCenterDbContext(string connectionString)
    {
        _client = new MongoClient(connectionString);
        _db = _client.GetDatabase("CustomerCenter");

        Basket = _db.GetCollection<Basket>("Basket");
        ShopHistory = _db.GetCollection<ShopHistory>("History");
        Customer = _db.GetCollection<Customer>("Customer");
    }

    public IMongoCollection<Basket> Basket { get; set; }
    public IMongoCollection<ShopHistory> ShopHistory { get; set; }
    public IMongoCollection<Customer> Customer { get; set; }
}