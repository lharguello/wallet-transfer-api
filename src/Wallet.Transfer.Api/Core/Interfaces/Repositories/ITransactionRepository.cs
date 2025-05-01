using WalletTransfer.Api.Core.Entities;

namespace WalletTransfer.Api.Core.Interfaces.Repositories;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
    Task<List<Transaction>> GetAllByWalletIdAsync(Guid walletId);
}