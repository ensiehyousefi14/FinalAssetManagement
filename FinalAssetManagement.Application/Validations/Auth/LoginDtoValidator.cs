using FinalAssetManagement.Application.DTOs.Auth;
using FluentValidation;

namespace FinalAssetManagement.Application.Validations.Auth
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty()
                                    .WithMessage("UserName is required.")
                                    .MaximumLength(50)
                                    .WithMessage("UserName cannot exceed 50 characters.");

            RuleFor(x => x.Password).NotEmpty()
                                    .WithMessage("Password is required.");
        }
    }
}
