using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerCenter.Core;

public static class CustomerCenterStartup
{
    public static IServiceCollection RegisterCustomerCenterServices(this IServiceCollection service)
    {
        return service;
    }
}