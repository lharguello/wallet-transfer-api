using WalletTransfer.Api.Application.Exceptions;

namespace WalletTransfer.Api.Core.Entities;

public class Wallet : BaseEntity
{
    public string DocumentId { get; private set; }
    public string Name { get; private set; }
    public decimal Balance { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Wallet(string documentId, string name, decimal balance)
    {
        DocumentId = documentId;
        Name = name;
        Balance = balance;
        UpdatedAt = null;
    }

    public static Wallet Create(string documentId, string name)
    {
        if (string.IsNullOrWhiteSpace(documentId))
        {
            throw new BadRequestException("DocumentId cannot be empty.");
        }
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BadRequestException("Name cannot be empty.");
        }
        return new Wallet(documentId, name, 0);
    }

    public void UpdateDocumentId(string documentId)
    {
        if (string.IsNullOrWhiteSpace(documentId))
        {
            throw new BadRequestException("DocumentId cannot be empty.");
        }
        DocumentId = documentId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BadRequestException("Name cannot be empty.");
        }
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateBalance(decimal balance)
    {
        if (balance <= 0)
        {
            throw new BadRequestException("Balance must be greater than zero.");
        }
        Balance = balance;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Debit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new BadRequestException("Debit amount must be greater than zero.");
        }
        if (Balance < amount)
        {
            throw new BadRequestException("Insufficient balance for debit.");
        }
        Balance -= amount;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Credit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new BadRequestException("Credit amount must be greater than zero.");
        }
        Balance += amount;
        UpdatedAt = DateTime.UtcNow;
    }
}