using FluentValidation;

namespace WalletTransfer.Api.Application.Features.Wallets.CreateWallet;

public class CreateWalletValidator : AbstractValidator<CreateWalletCommand>
{
    public CreateWalletValidator()
    {
        RuleFor(p => p.DocumentId)
            .NotEmpty().WithMessage("The {PropertyName} field is required")
            .MaximumLength(50).WithMessage("The {PropertyName} must be a string with a maximum length of {MaxLength}");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("The {PropertyName} field is required")
            .MaximumLength(100).WithMessage("The {PropertyName} must be a string with a maximum length of {MaxLength}");
    }
}
