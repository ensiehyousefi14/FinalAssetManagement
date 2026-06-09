using FinalAssetManagement.Application.DTOs.User;
using FluentValidation;

namespace FinalAssetManagement.Application.Validations.User
{
    public class PatchUserDtoValidator : AbstractValidator<PatchUserDto>
    {
        public PatchUserDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty()
                                    .WithMessage("UserName is required.")
                                    .MaximumLength(50)
                                    .WithMessage("UserName cannot exceed 50 characters.")
                                    .When(x => x.UserName != null);

            RuleFor(x => x.Password).NotEmpty()
                                    .WithMessage("Password is required.")
                                    .MaximumLength(50)
                                    .WithMessage("Password cannot exceed 50 characters.")
                                    .When(x => x.Password != null);
        }
    }
}
