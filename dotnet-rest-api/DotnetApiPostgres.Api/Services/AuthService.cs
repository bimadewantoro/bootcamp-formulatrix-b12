using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotnetApiPostgres.Api.Models;
using DotnetApiPostgres.Api.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DotnetApiPostgres.Api.Services;

public interface IAuthService
{
    Task<UserResponseDTO?> RegisterAsync(RegisterUserDTO registerUserDto);
    Task<UserResponseDTO?> LoginAsync(LoginUserDTO loginUserDto);
}

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<UserResponseDTO?> RegisterAsync(RegisterUserDTO registerUserDto)
    {
        // Check if username or email already exists
        if (await _context.Users.AnyAsync(u => u.Username == registerUserDto.Username || u.Email == registerUserDto.Email))
        {
            return null;
        }

        // Create new user with hashed password
        var user = new User
        {
            Username = registerUserDto.Username,
            Email = registerUserDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Generate token
        var token = GenerateJwtToken(user);

        // Return user response
        return new UserResponseDTO
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Token = token
        };
    }

    public async Task<UserResponseDTO?> LoginAsync(LoginUserDTO loginUserDto)
    {
        // Find user by username
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginUserDto.Username);

        // Check if user exists and password is correct
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.PasswordHash))
        {
            return null;
        }

        // Generate token
        var token = GenerateJwtToken(user);

        // Return user response
        return new UserResponseDTO
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Token = token
        };
    }

    private string GenerateJwtToken(User user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? "DefaultSecretKeyThatShouldBeReplaced123!");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(6),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        return jwtTokenHandler.WriteToken(token);
    }
}