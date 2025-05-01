using MediatR;

namespace WalletTransfer.Api.Application.Features.Wallets.UpdateWallet;

/// <summary>
/// Update wallet
/// </summary>
public class UpdateWalletCommand : IRequest<Unit>
{
    internal Guid Id { get; set; }
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
    /// <summary>
    /// Balance
    /// </summary>
    /// <example>10.5</example>
    public decimal? Balance { get; set; }
}
