using FinalAssetManagement.Contract.Repositories;
using FinalAssetManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalAssetManagement.Infrastructure.Persistence.Repositories
{
    public class TransactionRepository(ApplicationDbContext context) : GenericRepository<Transaction>(context), ITransactionRepository
    {
        public async Task<IEnumerable<Transaction>> GetTransactionsByAssetIdAsync(int assetId)
        {
            return await _dbSet.Where(t => t.AssetId == assetId).AsNoTracking().ToListAsync();
        }
    }
}
