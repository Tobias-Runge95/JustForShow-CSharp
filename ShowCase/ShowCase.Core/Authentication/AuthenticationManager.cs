using ShowCase.AuthDatabase.Models;
using ShowCase.Core.Request.Authentication;

namespace ShowCase.Core.Authentication;

public class AuthenticationManager
{
    private readonly ApplicationDbContext _dbContext;

    public AuthenticationManager(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Login(Login login)
    {
        
    }
}