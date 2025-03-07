using DotnetApiPostgres.Api.Models.DTO;
using DotnetApiPostgres.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApiPostgres.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDTO registerUserDto)
    {
        try
        {
            var result = await _authService.RegisterAsync(registerUserDto);
            if (result == null)
            {
                return BadRequest("Username or email already exists");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDTO loginUserDto)
    {
        try
        {
            var result = await _authService.LoginAsync(loginUserDto);
            if (result == null)
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}