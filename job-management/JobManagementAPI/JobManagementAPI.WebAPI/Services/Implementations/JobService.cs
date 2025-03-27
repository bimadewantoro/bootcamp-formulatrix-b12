using AutoMapper;
using FluentValidation;
using JobManagementAPI.WebAPI.Data.Repositories.Interfaces;
using JobManagementAPI.WebAPI.Models.DTOs.Job;
using JobManagementAPI.WebAPI.Models;
using JobManagementAPI.WebAPI.Services.Interfaces;
using JobManagementAPI.WebAPI.Validators.Job;

namespace JobManagementAPI.WebAPI.Services.Implementations
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        private readonly CreateJobDtoValidator _createValidator;
        private readonly UpdateJobDtoValidator _updateValidator;

        public JobService(
            IJobRepository jobRepository,
            IMapper mapper,
            CreateJobDtoValidator createValidator,
            UpdateJobDtoValidator updateValidator)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<JobDto> CreateJobAsync(CreateJobDto createDto, string username)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var job = _mapper.Map<Job>(createDto);
            job.Id = Guid.NewGuid();
            job.CreatedAt = DateTime.UtcNow;
            job.CreatedBy = username;

            await _jobRepository.AddAsync(job);
            await _jobRepository.SaveChangesAsync();

            return _mapper.Map<JobDto>(job);
        }

        public async Task<IEnumerable<JobDto>> GetAllJobsAsync()
        {
            var jobs = await _jobRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<JobDto>>(jobs);
        }

        public async Task<IEnumerable<JobDto>> GetActiveJobsAsync()
        {
            var jobs = await _jobRepository.GetActiveJobsAsync();
            return _mapper.Map<IEnumerable<JobDto>>(jobs);
        }

        public async Task<IEnumerable<JobDto>> GetJobsByDepartmentAsync(string department)
        {
            var jobs = await _jobRepository.GetJobsByDepartmentAsync(department);
            return _mapper.Map<IEnumerable<JobDto>>(jobs);
        }

        public async Task<JobDto> GetJobByIdAsync(Guid id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            return _mapper.Map<JobDto>(job);
        }

        public async Task<bool> UpdateJobAsync(Guid id, UpdateJobDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var job = await _jobRepository.GetByIdAsync(id);
            if (job == null)
                return false;

            _mapper.Map(updateDto, job);
            job.UpdatedAt = DateTime.UtcNow;

            _jobRepository.Update(job);
            return await _jobRepository.SaveChangesAsync();
        }

        public async Task<bool> ToggleJobStatusAsync(Guid id, bool isActive)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            if (job == null)
                return false;

            job.IsActive = isActive;
            job.UpdatedAt = DateTime.UtcNow;

            _jobRepository.Update(job);
            return await _jobRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteJobAsync(Guid id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            if (job == null)
                return false;

            _jobRepository.Remove(job);
            return await _jobRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobDto>> GetLatestJobsAsync()
        {
            var jobs = await _jobRepository.GetLatestJobsAsync();
            return _mapper.Map<IEnumerable<JobDto>>(jobs);
        }
    }
}