using FinalAssetManagement.Contract.Repositories;
using FinalAssetManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalAssetManagement.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository(ApplicationDbContext context) : GenericRepository<Category>(context), ICategoryRepository
    {
        public async Task<Category?> GetCategoryWithAssetsAsync(int categoryId)
        {
            return await _dbSet.Include(c => c.Assets)
                               .AsNoTracking()
                               .FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task<Category?> GetCategoryWithCompleteAssetsAsync(int categoryId)
        {
            return await _dbSet.Include(c => c.Assets)
                                  .ThenInclude(a => a.User)
                               .Include(c => c.Assets)
                                  .ThenInclude(a => a.Transactions)
                               .AsNoTracking()
                               .FirstOrDefaultAsync(c => c.Id == categoryId);
        }
    }
}
