using FluentValidation;
using JobManagementAPI.WebAPI.Models.DTOs.Auth;

namespace JobManagementAPI.WebAPI.Validators.Auth
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}