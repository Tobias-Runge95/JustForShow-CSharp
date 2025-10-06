using CustomerCenter.Core.Manager;
using CustomerCenter.Core.RequestDTOs.Customer;
using Microsoft.AspNetCore.Mvc;

namespace WebWorkPlace.API.Controller.CustomerCenter;

[Controller, Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerManager _customerManager;

    public CustomerController(ICustomerManager customerManager)
    {
        _customerManager = customerManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomer([FromQuery] GetCustomer getCustomer) =>
        Ok(await _customerManager.GetCustomer(getCustomer));

    [HttpPatch("/update")]
    public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomer updateCustomer)
    {
        await _customerManager.UpdateCustomer(updateCustomer);
        return Ok();
    }
}