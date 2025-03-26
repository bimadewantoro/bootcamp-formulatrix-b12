using JobManagementAPI.WebAPI.Models;

namespace JobManagementAPI.WebAPI.Data.Repositories.Interfaces
{
    public interface IJobRepository : IRepository<Job>
    {
        Task<IEnumerable<Job>> GetActiveJobsAsync();
        Task<IEnumerable<Job>> GetJobsByDepartmentAsync(string department);
    }
}