using Microsoft.AspNetCore.Mvc;
using ProductCenter.Core.Manager;

namespace WebWorkPlace.API.Controller.ProductCenter;

[Controller, Route("api/[controller]")]
public class FoodProductController : ControllerBase
{
    private readonly IFoodProductManager _foodProductManager;

    public FoodProductController(IFoodProductManager foodProductManager)
    {
        _foodProductManager = foodProductManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateFoodProduct()
    {
        _foodProductManager.CreateFoodProduct();

        return Ok();
    }
}