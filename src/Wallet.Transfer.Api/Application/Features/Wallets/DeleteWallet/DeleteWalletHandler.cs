using MediatR;
using WalletTransfer.Api.Application.Exceptions;
using WalletTransfer.Api.Core.Interfaces.Repositories;

namespace WalletTransfer.Api.Application.Features.Wallets.DeleteWallet;

public class DeleteWalletCommandHandler(IWalletRepository walletRepository) : IRequestHandler<DeleteWalletCommand, Unit>
{
    public async Task<Unit> Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
    {
        var walletToDelete = await walletRepository.GetByIdAsync(request.Id);

        if (walletToDelete is null)
        {
            throw new DataNotFoundException("Wallet not found");
        }

        try
        {
            await walletRepository.DeleteAsync(request.Id);
            return Unit.Value;
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException($"Error deleting wallet: {ex.Message}");
        }
    }
}