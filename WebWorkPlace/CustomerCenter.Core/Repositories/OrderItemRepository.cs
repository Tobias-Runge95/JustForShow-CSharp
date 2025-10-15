using CustomerCenter.Database;
using CustomerCenter.Database.Models;

namespace CustomerCenter.Core.Repositories;

public interface IOrderItemRepository : IBaseRepository<OrderItem>
{
    
}

public class OrderItemRepository :  BaseRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(CustomerCenterDbContext db) : base(db)
    {
    }
}