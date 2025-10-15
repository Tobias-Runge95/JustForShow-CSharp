using CustomerCenter.Database;
using CustomerCenter.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerCenter.Core.Repositories;

public interface IPeopleRepository : IBaseRepository<People>
{
    Task<People?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}

public class PeopleRepository :  BaseRepository<People>, IPeopleRepository
{
    public PeopleRepository(CustomerCenterDbContext db) : base(db)
    {
    }

    public async Task<People?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Peoples
            .Where(x => x.Id == id)
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}