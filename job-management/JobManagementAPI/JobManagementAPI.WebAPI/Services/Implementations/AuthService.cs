using AutoMapper;
using JobManagementAPI.WebAPI.Data.Repositories.Interfaces;
using JobManagementAPI.WebAPI.Models.DTOs.Auth;
using JobManagementAPI.WebAPI.Helpers;
using JobManagementAPI.WebAPI.Models;
using JobManagementAPI.WebAPI.Services.Interfaces;

namespace JobManagementAPI.WebAPI.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;

        public AuthService(
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IMapper mapper,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _mapper = mapper;
            _jwtService = new JwtService(configuration);
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterUserDto registerDto)
        {
            if (!await _userRepository.IsUsernameUniqueAsync(registerDto.Username))
                throw new Exception("Username is already taken");

            if (!await _userRepository.IsEmailUniqueAsync(registerDto.Email))
                throw new Exception("Email is already registered");

            PasswordHelper.CreatePasswordHash(registerDto.Password, out string passwordHash, out string salt);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                Salt = salt,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Role = "User",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return await GenerateAuthResponse(user);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginUserDto loginDto)
        {
            var user = await _userRepository.GetByUsernameAsync(loginDto.Username);

            if (user == null || !user.IsActive)
                throw new Exception("Invalid username or password");

            if (!PasswordHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.Salt))
                throw new Exception("Invalid username or password");

            return await GenerateAuthResponse(user);
        }

        public async Task<bool> LogoutAsync(string refreshToken)
        {
            var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            
            if (token == null)
                return false;

            token.IsRevoked = true;
            return await _refreshTokenRepository.SaveChangesAsync();
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            
            if (token == null || token.IsRevoked || token.ExpiryDate < DateTime.UtcNow)
                throw new Exception("Invalid refresh token");

            var user = token.User;
            
            token.IsRevoked = true;
            await _refreshTokenRepository.SaveChangesAsync();
            
            return await GenerateAuthResponse(user);
        }

        private async Task<AuthResponseDto> GenerateAuthResponse(User user)
        {
            var jwtToken = _jwtService.GenerateJwtToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();
            var expiryTime = _jwtService.GetRefreshTokenExpiryTime();

            var refreshTokenEntity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = refreshToken,
                ExpiryDate = expiryTime,
                UserId = user.Id,
                IsRevoked = false
            };

            await _refreshTokenRepository.AddAsync(refreshTokenEntity);
            await _refreshTokenRepository.SaveChangesAsync();

            return new AuthResponseDto
            {
                AccessToken = jwtToken,
                RefreshToken = refreshToken,
                ExpiresAt = expiryTime,
                User = _mapper.Map<UserDto>(user)
            };
        }
    }
}