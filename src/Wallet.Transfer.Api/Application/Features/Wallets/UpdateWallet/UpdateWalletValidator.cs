using FluentValidation;

namespace WalletTransfer.Api.Application.Features.Wallets.UpdateWallet;

public class UpdateWalletValidator : AbstractValidator<UpdateWalletCommand>
{
    public UpdateWalletValidator()
    {
        RuleFor(p => p.DocumentId)
            .MaximumLength(50).WithMessage("The {PropertyName} must be a string with a maximum length of {MaxLength}")
            .When(p => !string.IsNullOrEmpty(p.DocumentId));

        RuleFor(p => p.Name)
            .MaximumLength(100).WithMessage("The {PropertyName} must be a string with a maximum length of {MaxLength}")
            .When(p => !string.IsNullOrEmpty(p.Name));
    }
}