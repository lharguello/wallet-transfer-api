using MediatR;
using WalletTransfer.Api.Application.Wrappers;

namespace WalletTransfer.Api.Application.Features.Wallets.CreateWallet;

/// <summary>
/// Command to create a new wallet.
/// </summary>
public class CreateWalletCommand : IRequest<ApiSuccessResponse<CreatedResponse>>
{
    /// <summary>
    /// The document ID of the wallet owner.
    /// </summary>
    /// <example>12345</example>
    public string DocumentId { get; set; } = string.Empty;

    /// <summary>
    /// Wallet owner's name
    /// </summary>
    /// <example>Jhon Doe</example>
    public string Name { get; set; } = string.Empty;
}
