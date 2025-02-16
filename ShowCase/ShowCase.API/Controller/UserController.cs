using Microsoft.AspNetCore.Mvc;
using ShowCase.Core.Authentication;
using ShowCase.Core.Request.Authentication;

namespace ShowCase.API.Controller;

[Controller, Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager _userManager;

    public UserController(UserManager userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] Register register)
    {
        var result = await _userManager.RegisterUser(register);


        return Ok();
    }
}