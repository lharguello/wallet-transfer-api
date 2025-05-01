using MediatR;
using WalletTransfer.Api.Application.Wrappers;
using WalletTransfer.Api.Core.Entities;
using WalletTransfer.Api.Core.Interfaces.Repositories;

namespace WalletTransfer.Api.Application.Features.Wallets.CreateWallet;

public class CreateWalletHandler(IWalletRepository walletRepository) : IRequestHandler<CreateWalletCommand, ApiSuccessResponse<CreatedResponse>>
{
    public async Task<ApiSuccessResponse<CreatedResponse>> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
    {
        var newWallet = Wallet.Create(request.DocumentId, request.Name);

        await walletRepository.AddAsync(newWallet);
        return new ApiSuccessResponse<CreatedResponse>(new CreatedResponse { Id = newWallet.Id}, "successful");
    }
}
