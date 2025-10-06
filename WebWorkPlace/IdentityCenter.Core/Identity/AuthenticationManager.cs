using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Exceptions;
using WebWorkPlace.Core.MediatR.Commands.Authentication;
using WebWorkPlace.Core.Repositories;
using WebWorkPlace.Core.Response;
using WebWorkPlace.Core.Token;
using WebWorkPlace.Database;
using WebWorkPlace.Database.Models;

namespace WebWorkPlace.Core.Identity;

public interface IAuthenticationManager
{
    Task<LoginResponse> BaseLoginAsync(LoginRequest  loginRequest, CancellationToken cancellationToken);
    Task<LoginResponse> AppLoginAsync(AppLogin  loginRequest, CancellationToken cancellationToken);
    Task<RenewTokenResponse> RenewBaseTokenAsync(RenewTokenRequest renewTokenRequest, CancellationToken cancellationToken);
    Task<RenewTokenResponse> RenewAppTokenAsync(RenewAppTokenRequest renewTokenRequest, CancellationToken cancellationToken);
    Task LogoutBaseAsync(Guid userId, CancellationToken cancellationToken);
    Task LogoutAppAsync(LogoutAppRequest logoutRequest, CancellationToken cancellationToken);
}

public class AuthenticationManager : IAuthenticationManager
{
    private readonly UserManager _userManager;
    private readonly IAppUserRoleRepository _appUserRoleRepository;
    private readonly IAppRepository _appRepository;
    private readonly TokenService _tokenService;
    private readonly ApplicationDbContext _dbContext;

    public AuthenticationManager(UserManager userManager, IAppRepository appRepository, IAppUserRoleRepository appUserRoleRepository, TokenService tokenService, ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _appRepository = appRepository;
        _appUserRoleRepository = appUserRoleRepository;
        _tokenService = tokenService;
        _dbContext = dbContext;
    }

    public async Task<LoginResponse> BaseLoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserByEmailAsync(loginRequest.Email, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException("user not found");
        }
        
        bool isPasswordValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
        if (!isPasswordValid)
        {
            throw new InvalidCredentialsException("Invalid username or password.");
        }
        var claims = new  List<Claim>{new Claim(ClaimTypes.Name, "LoggedIn"),  new Claim(ClaimTypes.Name, "User"),
            new Claim(ClaimTypes.Actor, user.Id.ToString())};
        var token = await _tokenService.GenerateTokensAsync(claims);
        _dbContext.UserTokens.Add(new UserToken{Name = "Base", UserId = user.Id, Value = token.RenewToken, LoginProvider = "Base" });
        await _dbContext.SaveChangesAsync(cancellationToken);
        return token;
    }

    public async Task<LoginResponse> AppLoginAsync(AppLogin loginRequest, CancellationToken cancellationToken)
    {
        var app = await _appRepository.GetByName(loginRequest.AppName, cancellationToken);
        if (app is null)
        {
            throw new NotFoundException("App not found");
        }
        var roles = await _appUserRoleRepository.GetAllUserRolesForAppAsync(app.Id, loginRequest.UserId, cancellationToken);
        var token = await _tokenService.GenerateTokensAsync(roles.MapToClaim());
        _dbContext.UserTokens.Add(new UserToken{Name = loginRequest.AppName, UserId = loginRequest.UserId, Value = token.RenewToken, LoginProvider = loginRequest.AppName });
        await _dbContext.SaveChangesAsync(cancellationToken);
        return token;
    }

    public async Task<RenewTokenResponse> RenewBaseTokenAsync(RenewTokenRequest renewTokenRequest, CancellationToken cancellationToken)
    {
        var dbToken = await _dbContext.UserTokens.FirstOrDefaultAsync(t => t.UserId == renewTokenRequest.UserId &&  t.Name == "Base", cancellationToken: cancellationToken)
                      ??  throw new InvalidTokenException("Invalid token");
        ValidateToken((dbToken as UserToken)!, renewTokenRequest.RenewToken);
        var claims = new  List<Claim>{new Claim(ClaimTypes.Name, "LoggedIn"),  new Claim(ClaimTypes.Name, "User"),
            new Claim(ClaimTypes.Actor, renewTokenRequest.UserId.ToString())};
        var token = await _tokenService.GenerateTokensAsync(claims);
        return new RenewTokenResponse(){AuthToken = token.AuthToken};
    }

    public async Task<RenewTokenResponse> RenewAppTokenAsync(RenewAppTokenRequest renewTokenRequest, CancellationToken cancellationToken)
    {
        var dbToken = await _dbContext.UserTokens.FirstOrDefaultAsync(t => t.UserId == renewTokenRequest.UserId &&  t.Name == renewTokenRequest.AppName, cancellationToken)
                      ??  throw new InvalidTokenException("Invalid token");
        ValidateToken((dbToken as UserToken)!, renewTokenRequest.RenewToken);
        var app = await _appRepository.GetByName(renewTokenRequest.AppName, cancellationToken);
        var roles = await _appUserRoleRepository.GetAllUserRolesForAppAsync(app.Id, renewTokenRequest.UserId, cancellationToken);
        var token = await _tokenService.GenerateTokensAsync(roles.MapToClaim());
        return new RenewTokenResponse(){AuthToken = token.AuthToken};
    }

    public async Task LogoutBaseAsync(Guid userId, CancellationToken cancellationToken)
    {
        var dbTokens = await _dbContext.UserTokens.Where(t => t.UserId == userId).ToListAsync(cancellationToken);
        if (dbTokens.Count > 0)
        {
            _dbContext.UserTokens.RemoveRange(dbTokens);
            await _dbContext.SaveChangesAsync(cancellationToken); 
        }
    }

    public async Task LogoutAppAsync(LogoutAppRequest logoutRequest, CancellationToken cancellationToken)
    {
        var dbToken = await _dbContext.UserTokens
            .FirstOrDefaultAsync(t => t.UserId == logoutRequest.UserId && t.Name == logoutRequest.AppName, cancellationToken);
        if (dbToken is not null)
        {
            _dbContext.UserTokens.Remove(dbToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
    
    private void ValidateToken(UserToken token, string tokenToCheck)
    {
        var now = DateTime.UtcNow;
        if (token.Expiration.CompareTo(now) < 0)
        {
            throw new TokenExpiredException("Token has expired");
        }
        var result = String.Equals(token.Value, tokenToCheck, StringComparison.InvariantCultureIgnoreCase);
        if (!result)
        {
            throw new InvalidTokenException("Invalid token");
        }
    }
}