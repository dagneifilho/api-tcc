using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("auth")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationAppService _appService;

    public AuthController(IAuthenticationAppService appService)
    {
        _appService = appService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Credentials credentials)
    {
        if (!ModelState.IsValid)
            return StatusCode(400);
        var token = await _appService.LogIn(credentials);
        if (token.Token is null)
            return StatusCode(401);
        return StatusCode(200, token);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDTO user)
    {
        if (!ModelState.IsValid)
            return StatusCode(400);
        var result = await _appService.Register(user);
        if (string.IsNullOrEmpty(result))
            return StatusCode(201);
        return StatusCode(400, result);
    }
}