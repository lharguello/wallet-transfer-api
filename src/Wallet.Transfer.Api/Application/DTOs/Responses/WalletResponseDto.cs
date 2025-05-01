using System.Text.Json.Serialization;

namespace WalletTransfer.Api.Application.DTOs.Responses;

/// <summary>
/// Represents the response model for a Wallet
/// </summary>
public class WalletResponseDto
{
    /// <summary>
    /// The unique identifier of the wallet
    /// </summary>
    /// <example>a1b2c3d4-e5f6-7890-1234-567890abcdef</example>
    public Guid Id { get; set; }

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
    /// The current balance of the wallet.
    /// </summary>
    /// <example>500.75</example>
    public decimal Balance { get; set; }

    /// <summary>
    /// The date and time when the wallet was created.
    /// </summary>
    /// <example>2025-05-01T01:00:00Z</example>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The date and time when the wallet was last updated.
    /// </summary>
    /// <example>2025-05-01T01:03:30Z</example>
    public DateTime? UpdatedAt { get; set; }
}
