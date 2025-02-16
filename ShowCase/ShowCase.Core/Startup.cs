using Microsoft.Extensions.DependencyInjection;
using ShowCase.Core.Authentication;
using ShowCase.Core.Authorization;
using AuthenticationManager = System.Net.AuthenticationManager;

namespace ShowCase.Core;

public static class Startup
{
    public static IServiceCollection Register(this IServiceCollection service)
    {
        return service
            .AddScoped<AuthorizationManager>()
            .AddScoped<AuthenticationManager>()
            .AddScoped<UserManager>();
    }
}