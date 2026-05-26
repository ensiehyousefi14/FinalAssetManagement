using FinalAssetManagement.Contract.Repositories;
using FinalAssetManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalAssetManagement.Infrastructure.Persistence.Repositories
{
    public class AssetRepository(ApplicationDbContext context) : GenericRepository<Asset>(context), IAssetRepository
    {
        public async Task<IEnumerable<Asset>> GetAssetsByCategoryIdAsync(int categoryId)
        {
            return await _dbSet.Where(a => a.CategoryId == categoryId)
                               .Include(a => a.Category)
                               .Include(a => a.User)
                               .AsNoTracking()
                               .ToListAsync();
        }

        public async Task<IEnumerable<Asset>> GetAssetsByUserIdAsync(int userId)
        {
            return await _dbSet.Where(a => a.UserId == userId)
                               .Include(a => a.Category)
                               .Include(a => a.User)
                               .AsNoTracking()
                               .ToListAsync();
        }

        public async Task<Asset?> GetAssetWithCategoryAndUserAsync(int assetId)
        {
            return await _dbSet.Include(a => a.Category)
                               .Include(a => a.User)
                               .AsNoTracking()
                               .FirstOrDefaultAsync(a => a.Id == assetId);
        }

        public async Task<Asset?> GetAssetWithTransactionsAsync(int assetId)
        {
            return await _dbSet.Include(a => a.Transactions)
                               .AsNoTracking()
                               .FirstOrDefaultAsync(a => a.Id == assetId);
        }

        public async Task<Asset?> GetAssetWithDetailsAsync(int assetId)
        {
            return await _dbSet.Include(a => a.Category)
                               .Include(a => a.User)
                               .Include(a => a.Transactions)
                               .AsNoTracking()
                               .FirstOrDefaultAsync(a => a.Id == assetId);
        }

        public async Task<bool> HasTransactionsAsync(int assetId)
        {
            return await _dbSet.AnyAsync(a => a.Id == assetId && a.Transactions.Any());
        }
    }
}
