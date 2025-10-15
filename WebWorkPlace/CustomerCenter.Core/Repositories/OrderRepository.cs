using CustomerCenter.Database;
using CustomerCenter.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerCenter.Core.Repositories;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<Order?> GetById(Guid id, CancellationToken cancellationToken);
    Task<List<Order>> GetAll(CancellationToken cancellationToken);
    Task<List<Order>> GetByCustomerId(Guid customerId, CancellationToken cancellationToken);
}

public class OrderRepository :  BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(CustomerCenterDbContext db) : base(db)
    {
    }

    public async Task<Order?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Orders
            .Where(o => o.Id == id)
            .Include(x => x.Customer)
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.ProductionOrder)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<List<Order>> GetAll(CancellationToken cancellationToken)
    {
        return await _db.Orders.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<List<Order>> GetByCustomerId(Guid customerId, CancellationToken cancellationToken)
    {
        return await _db.Orders
            .Where(o => o.CustomerId == customerId)
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.ProductionOrder)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}