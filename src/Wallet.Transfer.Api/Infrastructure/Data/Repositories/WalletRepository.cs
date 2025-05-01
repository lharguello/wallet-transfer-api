using Microsoft.EntityFrameworkCore;
using WalletTransfer.Api.Core.Entities;
using WalletTransfer.Api.Core.Interfaces.Repositories;
using WalletTransfer.Api.Infrastructure.Data.EntityFramework;

namespace WalletTransfer.Api.Infrastructure.Data.Repositories;

public class WalletRepository(ApplicationDBContext dbContext) : GenericRepository<Wallet>(dbContext), IWalletRepository
{
    public async Task<Wallet?> GetByDocumentIdAsync(string documentId)
    {
        return await dbContext.Wallets
            .Where(t => t.DocumentId == documentId)
            .FirstOrDefaultAsync();
    }
}