using FluentValidation;
using JobManagementAPI.WebAPI.Models.DTOs.Job;

namespace JobManagementAPI.WebAPI.Validators.Job
{
    public class CreateJobDtoValidator : AbstractValidator<CreateJobDto>
    {
        public CreateJobDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Job title is required")
                .MaximumLength(100).WithMessage("Job title cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Job description is required")
                .MaximumLength(2000).WithMessage("Job description cannot exceed 2000 characters");

            RuleFor(x => x.Department)
                .NotEmpty().WithMessage("Department is required")
                .MaximumLength(50).WithMessage("Department name cannot exceed 50 characters");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required")
                .MaximumLength(100).WithMessage("Location cannot exceed 100 characters");

            RuleFor(x => x.JobType)
                .NotEmpty().WithMessage("Job type is required")
                .MaximumLength(50).WithMessage("Job type cannot exceed 50 characters");

            RuleFor(x => x.MaxSalary)
                .GreaterThan(x => x.MinSalary)
                .When(x => x.MinSalary.HasValue && x.MaxSalary.HasValue)
                .WithMessage("Maximum salary must be greater than minimum salary");
        }
    }
}