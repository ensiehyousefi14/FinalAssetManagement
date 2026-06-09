using FinalAssetManagement.Application.DTOs.User;
using FluentValidation;

namespace FinalAssetManagement.Application.Validations.User
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty()
                                    .WithMessage("UserName is required.")
                                    .MaximumLength(50)
                                    .WithMessage("UserName cannot exceed 50 characters.");

            RuleFor(x => x.Password).NotEmpty()
                                    .WithMessage("Password is required.")
                                    .MaximumLength(50)
                                    .WithMessage("Password cannot exceed 50 characters.");
        }
    }
}
