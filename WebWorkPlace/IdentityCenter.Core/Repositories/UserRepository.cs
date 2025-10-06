using Microsoft.EntityFrameworkCore;
using WebWorkPlace.Database;
using WebWorkPlace.Database.Models;

namespace WebWorkPlace.Core.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetUserAsync(Guid userId, CancellationToken cancellationToken);
    Task<User?> GetUserByEmailAsync(string username, CancellationToken cancellationToken);
}

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext db) : base(db)
    {
    }

    public async Task<User?> GetUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _db.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User?> GetUserByEmailAsync(string username, CancellationToken cancellationToken)
    {
        return await _db.Users
            .FirstOrDefaultAsync(u => u.NormalizedUserName == username.ToUpper());
    }
}