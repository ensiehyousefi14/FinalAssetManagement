using FinalAssetManagement.Application.DTOs.Auth;
using FluentValidation;

namespace FinalAssetManagement.Application.Validations.Auth
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty()
                                    .WithMessage("UserName is required.")
                                    .MaximumLength(50)
                                    .WithMessage("UserName cannot exceed 50 characters.");

            RuleFor(x => x.Password).NotEmpty()
                                    .WithMessage("Password is required.")
                                    .MinimumLength(8)
                                    .WithMessage("Password must be at least 8 characters. ")
                                    .MaximumLength(50)
                                    .WithMessage("Password cannot exceed 50 characters.");
        }
    }
}
