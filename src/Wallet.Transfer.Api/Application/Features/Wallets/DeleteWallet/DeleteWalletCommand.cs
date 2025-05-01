using MediatR;

namespace WalletTransfer.Api.Application.Features.Wallets.DeleteWallet;

public class DeleteWalletCommand : IRequest<Unit>
{
    internal Guid Id { get; set; }
}
