using FluentValidation;
using FinalAssetManagement.Application.DTOs.Asset;

namespace FinalAssetManagement.Application.Validations.Asset
{
    public class UpdateAssetDtoValidator : AbstractValidator<UpdateAssetDto>
    {
        public UpdateAssetDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                                .WithMessage("Asset name is required. ")
                                .MaximumLength(50)
                                .WithMessage("Asset name cannot exceed 50 characters.");

            RuleFor(x => x.Price).GreaterThanOrEqualTo(0)
                                 .WithMessage("Asset price cannot be negative.");

            RuleFor(x => x.CategoryId).GreaterThan(0)
                                      .WithMessage("Category must be selected.");

            RuleFor(x => x.UserId).GreaterThan(0)
                                  .WithMessage("User must be selected.");
        }
    }
}
