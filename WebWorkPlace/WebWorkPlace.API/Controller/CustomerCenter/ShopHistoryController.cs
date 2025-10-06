using CustomerCenter.Core.Manager;
using CustomerCenter.Core.RequestDTOs.ShopHistory;
using Microsoft.AspNetCore.Mvc;

namespace WebWorkPlace.API.Controller.CustomerCenter;

[Controller, Route("api/[controller]")]
public class ShopHistoryController : ControllerBase
{
    private readonly IShopHistoryManager _shopHistoryManager;

    public ShopHistoryController(IShopHistoryManager shopHistoryManager)
    {
        _shopHistoryManager = shopHistoryManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetShopHistory([FromQuery] GetShopHistory getShopHistory)
        => Ok(await _shopHistoryManager.GetShopHistory(getShopHistory));
}