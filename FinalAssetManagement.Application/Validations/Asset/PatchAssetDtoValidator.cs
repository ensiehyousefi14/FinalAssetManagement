using FinalAssetManagement.Application.DTOs.Asset;
using FluentValidation;

namespace FinalAssetManagement.Application.Validations.Asset
{
    public class PatchAssetDtoValidator : AbstractValidator<PatchAssetDto>
    {
        public PatchAssetDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                                .WithMessage("Asset name is required.")
                                .MaximumLength(50)
                                .WithMessage("Asset cannot exceed 50 characters.")
                                .When(x => x.Name != null);

            RuleFor(x => x.Price).GreaterThanOrEqualTo(0)
                                 .WithMessage("Price cannot be negative.")
                                 .When(x => x.Price.HasValue);

            RuleFor(x => x.CategoryId).GreaterThan(0)
                                      .WithMessage("Please select a valid Category.")
                                      .When(x => x.CategoryId.HasValue);

            RuleFor(x => x.UserId).GreaterThan(0)
                                  .WithMessage("Please select a valid User.")
                                  .When(x => x.UserId.HasValue);
        }
    }
}
