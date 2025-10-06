using System.Reflection;
using CustomerCenter.Core.CustomerCenterService;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerCenter.Core;

public static class CustomerCenterStartup
{
    public static IServiceCollection RegisterCustomerCenterServices(this IServiceCollection service)
    {
        return service
            .AddSingleton<CustomerCenterDbContextFactory>();
    }
}