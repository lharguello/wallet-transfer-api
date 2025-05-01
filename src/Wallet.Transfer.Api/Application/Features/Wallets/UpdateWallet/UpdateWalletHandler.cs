using MediatR;
using WalletTransfer.Api.Application.Exceptions;
using WalletTransfer.Api.Core.Entities;
using WalletTransfer.Api.Core.Interfaces.Repositories;

namespace WalletTransfer.Api.Application.Features.Wallets.UpdateWallet;

public class UpdateWalletHandler(IWalletRepository walletRepository) : IRequestHandler<UpdateWalletCommand, Unit>
{
    public async Task<Unit> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
    {
        Wallet existingWallet = await walletRepository.GetByIdAsync(request.Id);

        if (existingWallet == null)
        {
            throw new DataNotFoundException("Wallet not found");
        }

        if (!string.IsNullOrWhiteSpace(request.DocumentId))
        {
            existingWallet.UpdateDocumentId(request.DocumentId);
        }

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            existingWallet.UpdateName(request.Name);
        }

        if (request.Balance is not null && request.Balance > 0)
        {
            existingWallet.UpdateBalance((decimal)request.Balance);
        }

        await walletRepository.UpdateAsync(existingWallet);

        return Unit.Value;
    }
}
