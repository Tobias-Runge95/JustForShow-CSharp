using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebWorkPlace.Core.Identity;
using WebWorkPlace.Core.MediatR.Commands.Authentication;

namespace IdentityCenter.API.Controller;

[Controller, Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationManager _authenticationManager;

    public AuthenticationController(IAuthenticationManager authenticationManager)
    {
        _authenticationManager = authenticationManager;
    }

    [HttpPost, Route("login")]
    public async Task<IActionResult> BaseLogin([FromBody] LoginRequest  loginRequest, CancellationToken cancellationToken)
    {
        var result = await _authenticationManager.BaseLoginAsync(loginRequest, cancellationToken);
        return Ok(result);
    }

    [HttpPost, Route("login-app"), Authorize(Roles = "user")]
    public async Task<IActionResult> BaseLoginApp([FromBody] AppLogin loginRequest, CancellationToken cancellationToken)
    {
        var result = await _authenticationManager.AppLoginAsync(loginRequest, cancellationToken);
        return Ok(result);
    }

    [HttpPatch, Route("renew"), Authorize(Roles = "user")]
    public async Task<IActionResult> RenewBaseToken([FromBody] RenewTokenRequest tokenRequest, CancellationToken cancellationToken)
    {
        var result = await _authenticationManager.RenewBaseTokenAsync(tokenRequest, cancellationToken);
        return Ok(result);
    }

    [HttpPatch, Route("renew-app"), Authorize(Roles = "user")]
    public async Task<IActionResult> RenewAppToken([FromBody] RenewAppTokenRequest tokenRequest, CancellationToken cancellationToken)
    {
        var result = await _authenticationManager.RenewAppTokenAsync(tokenRequest,  cancellationToken);
        return Ok(result);
    }

    [HttpDelete, Route("logout"), Authorize(Roles = "user")]
    public async Task<IActionResult> BaseLogout([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        await _authenticationManager.LogoutBaseAsync(userId, cancellationToken);
        return Ok();
    }

    [HttpDelete, Route("logout-app"), Authorize(Roles = "user")]
    public async Task<IActionResult> AppLogout([FromBody] LogoutAppRequest request, CancellationToken cancellationToken)
    {
        await _authenticationManager.LogoutAppAsync(request, cancellationToken);
        return Ok();
    }
}