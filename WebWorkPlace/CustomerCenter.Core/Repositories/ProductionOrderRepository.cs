using CustomerCenter.Database;
using CustomerCenter.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerCenter.Core.Repositories;

public interface IProductionOrderRepository : IBaseRepository<ProductionOrder>
{
    Task<ProductionOrder?> GetProductionOrder(Guid id, CancellationToken token);
}

public class ProductionOrderRepository : BaseRepository<ProductionOrder>, IProductionOrderRepository
{
    public ProductionOrderRepository(CustomerCenterDbContext db) : base(db)
    {
    }

    public async Task<ProductionOrder?> GetProductionOrder(Guid id, CancellationToken token)
    {
        return await _db.ProductionOrders
            .Where(x => x.Id == id)
            .Include(x => x.OrderItem)
            .FirstOrDefaultAsync(cancellationToken: token);
    }
}