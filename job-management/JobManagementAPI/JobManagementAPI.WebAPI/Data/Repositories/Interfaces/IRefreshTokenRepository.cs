using JobManagementAPI.WebAPI.Models;

namespace JobManagementAPI.WebAPI.Data.Repositories.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken> GetByTokenAsync(string token);
        Task RevokeAllUserTokensAsync(Guid userId);
    }
}