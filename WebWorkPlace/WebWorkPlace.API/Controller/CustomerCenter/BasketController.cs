using CustomerCenter.Core.Manager;
using CustomerCenter.Core.RequestDTOs.Basket;
using Microsoft.AspNetCore.Mvc;

namespace WebWorkPlace.API.Controller.CustomerCenter;

[Controller, Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketManager _basketManager;

    public BasketController(IBasketManager basketManager)
    {
        _basketManager = basketManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBasket([FromBody] CreateBasket createBasket)
    {
        await _basketManager.CreateBasket(createBasket);
        return Ok();
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateBasket([FromBody] UpdateBasket updateBasket)
    {
        await _basketManager.UpdateBasket(updateBasket);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetBasket([FromQuery] GetBasket getBasket)
        => Ok(await _basketManager.GetBasket(getBasket));
}