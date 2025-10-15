using CustomerCenter.Database;
using CustomerCenter.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerCenter.Core.Repositories;

public interface ICustomerRepository : IBaseRepository<Customer>
{
    Task<Customer?> GetById(Guid id, CancellationToken cancellationToken);
    Task<Customer?> GetByName(string name, CancellationToken cancellationToken);
    Task<List<Customer>> GetAll(CancellationToken cancellationToken);
    Task<bool> Exists(string name, CancellationToken cancellationToken);
}

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(CustomerCenterDbContext db) : base(db)
    {
    }

    public async Task<Customer?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Customers
            .Where(c => c.Id == id)
            .Include(x => x.Contacts)
            .Include(x => x.Orders)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<Customer?> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _db.Customers
            .Where(x => x.Name == name)
            .Include(x => x.Contacts)
            .Include(x => x.Orders)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            
    }

    public async Task<List<Customer>> GetAll(CancellationToken cancellationToken)
    {
        return await _db.Customers.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> Exists(string name, CancellationToken cancellationToken)
    {
        return await _db.Customers.AnyAsync(x => x.Name == name, cancellationToken);
    }
}