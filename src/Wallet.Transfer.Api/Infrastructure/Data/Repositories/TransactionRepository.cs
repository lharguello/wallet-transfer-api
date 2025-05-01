using Microsoft.EntityFrameworkCore;
using WalletTransfer.Api.Core.Entities;
using WalletTransfer.Api.Core.Interfaces.Repositories;
using WalletTransfer.Api.Infrastructure.Data.EntityFramework;

namespace WalletTransfer.Api.Infrastructure.Data.Repositories;

public class TransactionRepository(ApplicationDBContext dbContext) : GenericRepository<Transaction>(dbContext), ITransactionRepository
{
    public async Task<List<Transaction>> GetAllByWalletIdAsync(Guid walletId)
    {
        return await dbContext.Transactions
            .Where(t => t.WalletId == walletId).OrderByDescending(p=>p.CreatedAt)
            .ToListAsync();
    }
}
