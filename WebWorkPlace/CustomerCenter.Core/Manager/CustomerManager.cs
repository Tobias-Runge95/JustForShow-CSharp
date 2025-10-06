using System.Collections.Immutable;
using CustomerCenter.Core.Contracts.Customer;
using CustomerCenter.Core.CustomerCenterService;
using CustomerCenter.Core.RequestDTOs.Customer;
using CustomerCenter.Core.ResponseDTOs.Customer;
using CustomerCenter.Database;
using CustomerCenter.Database.Models;
using MongoDB.Driver;

namespace CustomerCenter.Core.Manager;

public interface ICustomerManager
{
    Task CreateCustomer(CreateCustomer createCustomer);
    Task UpdateCustomer(UpdateCustomer updateCustomer);
    Task DeleteCustomer(DeleteCustomer deleteCustomer);
    Task<CustomerDTO> GetCustomer(GetCustomer getCustomer);
}

public class CustomerManager : ICustomerManager
{
    private readonly CustomerCenterDbContext _customerCenterDbContext;

    public CustomerManager(CustomerCenterDbContextFactory customerCenterDbContextFactory)
    {
        _customerCenterDbContext = customerCenterDbContextFactory.CreateCustomerCenterDbContext();
    }

    public async Task CreateCustomer(CreateCustomer createCustomer)
    {
        var newCustomer = createCustomer.MapToCustomer();
        newCustomer.CreatedAt = DateTime.Now;
        newCustomer.Id = Guid.NewGuid();
        await _customerCenterDbContext.Customer.InsertOneAsync(newCustomer);
    }

    public async Task UpdateCustomer(UpdateCustomer updateCustomer)
    {
        var filter = Builders<Customer>.Filter.Eq(c => c.UserId, updateCustomer.UserId);
        var updateDefinition = Builders<Customer>.Update.Combine(
            Builders<Customer>.Update.Set(x => x.UpdatedAt, DateTime.Now),
            Builders<Customer>.Update.Set(x => x.Country, updateCustomer.Country),
            Builders<Customer>.Update.Set(x => x.Currency, updateCustomer.Currency),
            Builders<Customer>.Update.Set(x => x.Street, updateCustomer.Street),
            Builders<Customer>.Update.Set(x => x.HouseNumber, updateCustomer.HouseNumber),
            Builders<Customer>.Update.Set(x => x.ZipCode, updateCustomer.ZipCode));
        await _customerCenterDbContext.Customer.UpdateOneAsync(filter, updateDefinition);
    }

    public async Task DeleteCustomer(DeleteCustomer deleteCustomer)
    {
        var filter = Builders<Customer>.Filter.Eq(c => c.UserId, deleteCustomer.UserId);
        await _customerCenterDbContext.Customer.DeleteOneAsync(filter);
    }

    public async Task<CustomerDTO> GetCustomer(GetCustomer getCustomer)
    {
        var filter = Builders<Customer>.Filter.Eq(c => c.UserId, getCustomer.CustomerId);
        var customer = await _customerCenterDbContext.Customer.Find(filter).FirstAsync();

        return new CustomerDTO
        {
            Country = customer.Country,
            Currency = customer.Currency,
            CustomerId = customer.Id,
            HouseNumber = customer.HouseNumber,
            Street = customer.Street,
            ZipCode = customer.ZipCode
        };
    }
}