using JobManagementAPI.WebAPI.Data.Repositories.Interfaces;
using JobManagementAPI.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobManagementAPI.WebAPI.Data.Repositories.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbSet
                .Include(u => u.RefreshTokens)
                .SingleOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbSet.SingleOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> IsUsernameUniqueAsync(string username)
        {
            return !await _dbSet.AnyAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return !await _dbSet.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }
    }
}