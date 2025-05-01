using WalletTransfer.Api.Core.Enums;

namespace WalletTransfer.Api.Application.DTOs.Responses;

/// <summary>
/// Transaction response
/// </summary>
public class TransactionResponseDto
{
    /// <summary>
    /// The unique identifier of the transaction
    /// </summary>
    /// <example>f8e7d6c5-b4a3-2109-8765-43210fedcba9</example>
    public Guid Id { get; set; }

    /// <summary>
    /// The ID of the wallet associated with the transaction
    /// </summary>
    /// <example>98765432-10fe-dcba-9876-543210fedcba</example>
    public Guid WalletId { get; set; }

    /// <summary>
    /// The amount of the transaction
    /// </summary>
    /// <example>25.50</example>
    public decimal Amount { get; set; }

    /// <summary>
    /// The type of the transaction:
    /// - credit
    /// - debit
    /// </summary>
    /// <example>credit</example>
    public TransactionType Type { get; set; }

    /// <summary>
    /// The date and time when the transaction record was created.
    /// </summary>
    /// <example>2025-05-01T02:00:00Z</example>
    public DateTime CreatedAt { get; set; }
}
