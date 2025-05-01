using MediatR;
using WalletTransfer.Api.Application.Wrappers;
using WalletTransfer.Api.Core.Enums;

namespace WalletTransfer.Api.Application.Features.Transactions.CreateTransaction;

/// <summary>
/// Create Transaction
/// </summary>
public class CreateTransactionCommand: IRequest<ApiSuccessResponse<CreatedResponse>>
{
    /// <summary>
    /// The ID of the wallet from which the money will be transferred.
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-426614174000</example>
    internal Guid SourceWalletId { get; set; }

    /// <summary>
    /// The ID of the wallet to which the money will be transferred.
    /// </summary>
    /// <example>bcdefa01-2345-6789-abcd-ef0123456789</example>
    public Guid DestinationWalletId { get; set; }

    /// <summary>
    /// The amount of the transaction.
    /// </summary>
    /// <example>10.50</example>
    public decimal Amount { get; set; }
}
