using FinalAssetManagement.Application.DTOs.Category;
using FluentValidation;

namespace FinalAssetManagement.Application.Validations.Category
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                                .WithMessage("Category name is required.")
                                .MaximumLength(100)
                                .WithMessage("Category name cannot exceed 100 characters.");
        }
    }
}
