using JobManagementAPI.WebAPI.Data.Repositories.Interfaces;
using JobManagementAPI.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobManagementAPI.WebAPI.Data.Repositories.Implementations
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        public JobRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Job>> GetActiveJobsAsync()
        {
            return await _dbSet.Where(j => j.IsActive).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByDepartmentAsync(string department)
        {
            return await _dbSet.Where(j => j.Department == department && j.IsActive).ToListAsync();
        }
    }
}