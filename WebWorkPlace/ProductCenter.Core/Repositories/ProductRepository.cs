using MongoDB.Driver;
using ProductCenter.Database;
using ProductCenter.Database.Models;

namespace ProductCenter.Core.Repositories;

public class ProductRepository
{
    private readonly IMongoCollection<Product> _productCollection;

    public ProductRepository(ProductCenterDBContext productCenterDbContext)
    {
        _productCollection = productCenterDbContext.Products;
    }

    private IQueryable<Product> Get() => _productCollection.AsQueryable();
}