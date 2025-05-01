using WalletTransfer.Api.Application.Exceptions;
using WalletTransfer.Api.Core.Enums;

namespace WalletTransfer.Api.Core.Entities;

public class Transaction : BaseEntity
{
    public Guid WalletId { get; private set; }
    public decimal Amount { get; private set; }
    public TransactionType Type { get; private set; }

    public virtual Wallet? Wallet { get; internal set; }

    private Transaction(Guid walletId, decimal amount, TransactionType type)
    {
        WalletId = walletId;
        Amount = amount;
        Type = type;
    }

    public static Transaction Create(Guid walletId, decimal amount, TransactionType type)
    {
        if (amount <= 0)
        {
            throw new BadRequestException("Transaction amount must be greater than zero.");
        }
        return new Transaction(walletId, amount, type);
    }
}