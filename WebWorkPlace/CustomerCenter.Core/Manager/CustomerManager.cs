using System.Collections.Immutable;
using CustomerCenter.Core.Contracts.Customer;
using CustomerCenter.Core.Repositories;
using CustomerCenter.Core.RequestDTOs.Customer;
using CustomerCenter.Core.ResponseDTOs.Customer;
using CustomerCenter.Database;

namespace CustomerCenter.Core.Manager;

public interface ICustomerManager
{
    Task CreateCustomer(CreateCustomer createCustomer, CancellationToken cancellationToken);
    Task UpdateCustomer(UpdateCustomer updateCustomer, CancellationToken cancellationToken);
    Task DeleteCustomer(DeleteCustomer deleteCustomer, CancellationToken cancellationToken);
    Task<CustomerDTO> GetCustomer(GetCustomer getCustomer, CancellationToken cancellationToken);
}

public class CustomerManager : ICustomerManager
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerManager(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }


    public async Task CreateCustomer(CreateCustomer createCustomer, CancellationToken cancellationToken)
    {
        var exists = await _customerRepository.Exists(createCustomer.Name, cancellationToken);
        if(exists) throw new Exception("Customer already exists"); // TODO: Create better Exception
        
    }

    public async Task UpdateCustomer(UpdateCustomer updateCustomer, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCustomer(DeleteCustomer deleteCustomer, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<CustomerDTO> GetCustomer(GetCustomer getCustomer, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}