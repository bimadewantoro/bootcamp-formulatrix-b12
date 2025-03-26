using JobManagementAPI.WebAPI.Models.DTOs.Auth;
using JobManagementAPI.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobManagementAPI.WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            return Created(string.Empty, result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            return Ok(result);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] LogoutDto logoutDto)
        {
            var result = await _authService.LogoutAsync(logoutDto.RefreshToken);
            if (result)
                return Ok(new { message = "Logged out successfully" });
            else
                return BadRequest(new { message = "Invalid token" });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            var result = await _authService.RefreshTokenAsync(refreshTokenDto.RefreshToken);
            return Ok(result);
        }
    }
}