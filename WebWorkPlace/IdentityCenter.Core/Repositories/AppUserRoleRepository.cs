using Microsoft.EntityFrameworkCore;
using WebWorkPlace.Core.DTO;
using WebWorkPlace.Database;
using WebWorkPlace.Database.Models;

namespace WebWorkPlace.Core.Repositories;

public interface IAppUserRoleRepository : IBaseRepository<AppUserRole>
{
    Task<List<AppWithRolesDto>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken);
    Task<List<AppUserRole>> GetAppRolesAsync(Guid appId, CancellationToken cancellationToken);
    Task<List<AppUserRole>> GetAllUserRolesForAppAsync(Guid appId, Guid userId, CancellationToken cancellationToken);
}

public class AppUserRoleRepository :  BaseRepository<AppUserRole>, IAppUserRoleRepository
{
    public AppUserRoleRepository(ApplicationDbContext db) : base(db)
    {
    }

    public async Task<List<AppWithRolesDto>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken)
    {
        var a = await _db.AppUserRoles
            .Where(x => x.UserId == userId)
            .Include(x => x.Role)
            .Include(x => x.App)
            .GroupBy(x => x.App.Name)
            .Select(g => new AppWithRolesDto
            {
                AppName = g.Key,
                Roles = g.Select(x => x.Role).ToList()
            })
            .ToListAsync(cancellationToken);
        return a;
    }

    public async Task<List<AppUserRole>> GetAppRolesAsync(Guid appId, CancellationToken cancellationToken)
    {
        return await _db.AppUserRoles
            .Where(x => x.AppId == appId)
            .Include(x => x.Role)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AppUserRole>> GetAllUserRolesForAppAsync(Guid appId, Guid userId, CancellationToken cancellationToken)
    {
        return await _db.AppUserRoles
            .Where(x => x.AppId == appId && x.UserId == userId)
            .Include(x => x.Role)
            .ToListAsync(cancellationToken);
    }
}