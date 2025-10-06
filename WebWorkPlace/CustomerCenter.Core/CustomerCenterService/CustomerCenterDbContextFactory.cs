using CustomerCenter.Database;
using System.Configuration;
using Microsoft.Extensions.Configuration;


namespace CustomerCenter.Core.CustomerCenterService;

public class CustomerCenterDbContextFactory
{
    private readonly IConfiguration _config;

    public CustomerCenterDbContextFactory(IConfiguration config)
    {
        _config = config;
    }

    public CustomerCenterDbContext CreateCustomerCenterDbContext()
    {
        string connectionString = _config["ConnectionStrings:CustomerCenterConnectionString"];
        return new CustomerCenterDbContext(connectionString);
    }
}