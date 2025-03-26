using JobManagementAPI.WebAPI.Models.DTOs.Job;

namespace JobManagementAPI.WebAPI.Services.Interfaces
{
    public interface IJobService
    {
        Task<JobDto> CreateJobAsync(CreateJobDto createDto, string username);
        Task<IEnumerable<JobDto>> GetAllJobsAsync();
        Task<IEnumerable<JobDto>> GetActiveJobsAsync();
        Task<IEnumerable<JobDto>> GetJobsByDepartmentAsync(string department);
        Task<JobDto> GetJobByIdAsync(Guid id);
        Task<bool> UpdateJobAsync(Guid id, UpdateJobDto updateDto);
        Task<bool> ToggleJobStatusAsync(Guid id, bool isActive);
        Task<bool> DeleteJobAsync(Guid id);
    }
}