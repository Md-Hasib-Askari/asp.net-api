using Microsoft.AspNetCore.Mvc;
using ContactApi.Services;
using ContactApi.DTOs.Auth;

namespace ContactApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        this._authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        try
        {
            var user = _authService.Login(dto);
            if (user == null)
                return Unauthorized("Invalid username or password.");

            var token = _authService.GenerateJwtToken(user);

            return Ok(new { User = user, Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDto dto)
    {
        try
        {
            var result = _authService.Register(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}