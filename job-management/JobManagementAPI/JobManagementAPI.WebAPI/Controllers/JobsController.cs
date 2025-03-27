using System.Security.Claims;
using JobManagementAPI.WebAPI.Models.DTOs.Job;
using JobManagementAPI.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobManagementAPI.WebAPI.Controllers
{
    [Route("api/jobs")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> CreateJob([FromBody] CreateJobDto createDto)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var result = await _jobService.CreateJobAsync(createDto, username);
            return CreatedAtAction(nameof(GetJobById), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobDto>>> GetAllJobs([FromQuery] bool activeOnly = false)
        {
            if (activeOnly)
            {
                var activeJobs = await _jobService.GetActiveJobsAsync();
                return Ok(activeJobs);
            }
            else
            {
                var allJobs = await _jobService.GetAllJobsAsync();
                return Ok(allJobs);
            }
        }

        [HttpGet("department/{department}")]
        public async Task<ActionResult<IEnumerable<JobDto>>> GetJobsByDepartment(string department)
        {
            var jobs = await _jobService.GetJobsByDepartmentAsync(department);
            return Ok(jobs);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<JobDto>> GetJobById(Guid id)
        {
            var result = await _jobService.GetJobByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> UpdateJob(Guid id, [FromBody] UpdateJobDto updateDto)
        {
            var result = await _jobService.UpdateJobAsync(id, updateDto);
            if (!result)
                return NotFound();

            return Ok(new { message = "Job updated successfully" });
        }

        [HttpPatch("{id:guid}/status")]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> ToggleJobStatus(Guid id, [FromBody] ToggleJobStatusDto statusDto)
        {
            var result = await _jobService.ToggleJobStatusAsync(id, statusDto.IsActive);
            if (!result)
                return NotFound();

            return Ok(new { message = $"Job status updated to {(statusDto.IsActive ? "active" : "inactive")}" });
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            var result = await _jobService.DeleteJobAsync(id);
            if (!result)
                return NotFound();

            return Ok(new { message = "Job deleted successfully" });
        }
    }
}