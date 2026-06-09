using FinalAssetManagement.Application.DTOs.Category;
using FluentValidation;

namespace FinalAssetManagement.Application.Validations.Category
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                                .WithMessage("Category name is required.")
                                .MaximumLength(100)
                                .WithMessage("Category cannot exceed 100 characters.");
        }
    }
}
