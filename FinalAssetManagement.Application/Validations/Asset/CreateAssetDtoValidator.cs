using FinalAssetManagement.Application.DTOs.Asset;
using FluentValidation;

namespace FinalAssetManagement.Application.Validations.Asset
{
    public class CreateAssetDtoValidator : AbstractValidator<CreateAssetDto>
    {
        public CreateAssetDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                                .WithMessage("Asset name is required.")
                                .MaximumLength(50)
                                .WithMessage("Asset name cannot exceed 50 characters.");

            RuleFor(x => x.Price).GreaterThanOrEqualTo(0)
                                 .WithMessage("Asset price cannot be negative.");

            RuleFor(x => x.CategoryId).GreaterThan(0)
                                      .WithMessage("Please select a valid Category.");

            RuleFor(x => x.UserId).GreaterThan(0)
                                  .WithMessage("Please select a valid User.");
        }
    }
}
