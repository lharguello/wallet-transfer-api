using FluentValidation;
using WalletTransfer.Api.Core.Interfaces.Repositories;

namespace WalletTransfer.Api.Application.Features.Transactions.CreateTransaction;

public class CreateTransactionValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionValidator()
    {        
        RuleFor(p => p.DestinationWalletId)
            .NotEmpty().WithMessage("The {PropertyName} field is required");

        RuleFor(p => p.Amount)
            .NotEmpty().WithMessage("The {PropertyName} field is required")
            .GreaterThan(0).WithMessage("The {PropertyName} must be greater than zero");
    }
}