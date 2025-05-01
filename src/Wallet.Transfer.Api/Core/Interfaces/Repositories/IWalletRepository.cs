using WalletTransfer.Api.Core.Entities;

namespace WalletTransfer.Api.Core.Interfaces.Repositories;

public interface IWalletRepository : IGenericRepository<Wallet>
{
    Task<Wallet?> GetByDocumentIdAsync(string documentId);
}