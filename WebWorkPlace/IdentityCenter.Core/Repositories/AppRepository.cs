using Microsoft.EntityFrameworkCore;
using WebWorkPlace.Database;
using WebWorkPlace.Database.Models;

namespace WebWorkPlace.Core.Repositories;

public interface IAppRepository : IBaseRepository<App>
{
    Task<App?> GetById(Guid id, CancellationToken cancellationToken);
    Task<App?> GetByName(string name, CancellationToken cancellationToken);
}

public class AppRepository : BaseRepository<App>, IAppRepository
{
    public AppRepository(ApplicationDbContext db) : base(db)
    {
    }

    public async Task<App?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Apps
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<App?> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _db.Apps
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }
}