using FinalAssetManagement.Contract.Repositories;
using FinalAssetManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalAssetManagement.Infrastructure.Persistence.Repositories
{
    public class TransactionRepository(ApplicationDbContext context) : GenericRepository<Transaction>(context), ITransactionRepository
    {
        public async Task<IEnumerable<Transaction>> GetTransactionsWithAssetAsync()
        {
            return await _dbSet.Include(t => t.Asset)
                               .AsNoTracking()
                               .ToListAsync();
        }

        public async Task<Transaction?> GetTransactionWithAssetAsync(int transactionId)
        {
            return await _dbSet.Include(t => t.Asset)
                               .AsNoTracking()
                               .FirstOrDefaultAsync(t => t.Id == transactionId);

        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByAssetIdAsync(int assetId)
        {
            return await _dbSet.Where(t => t.AssetId == assetId)
                               .Include(t => t.Asset)
                               .AsNoTracking()
                               .ToListAsync();
        }
    }
}
