using FinalAssetManagement.Application.DTOs.Transaction;
using FluentValidation;

namespace FinalAssetManagement.Application.Validations.Transaction
{
    public class UpdateTransactionDtoValidator : AbstractValidator<UpdateTransactionDto>
    {
        public UpdateTransactionDtoValidator()
        {
            RuleFor(x => x.Description).NotEmpty()
                                       .WithMessage("Description is required.")
                                       .MaximumLength(200)
                                       .WithMessage("Description cannot exceed 200 characters.");

            RuleFor(x => x.Amount).NotEmpty()
                                  .WithMessage("Amount is required.")
                                  .GreaterThan(0)
                                  .WithMessage("Amount must be greater than zero.")
                                  .PrecisionScale(18, 2, true)
                                  .WithMessage("Amount cannot have more than 18 digits with 2 decimal places.");

            RuleFor(x => x.TransactionType).NotEmpty()
                                           .WithMessage("TransactionType is required.")
                                           .IsInEnum()
                                           .WithMessage("Invalid TransactionType.");

            RuleFor(x => x.AssetId).NotEmpty()
                                   .WithMessage("Asset is required for transaction.")
                                   .GreaterThan(0)
                                   .WithMessage("Valid Asset is required for transaction.");
        }
    }
}
