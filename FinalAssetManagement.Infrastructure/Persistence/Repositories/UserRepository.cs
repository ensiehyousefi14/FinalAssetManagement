using FinalAssetManagement.Contract.Repositories;
using FinalAssetManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalAssetManagement.Infrastructure.Persistence.Repositories
{
    public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context), IUserRepository
    {
        public async Task<User?> GetUserWithAssetsAsync(int userId)
        {
            return await _dbSet.Include(u => u.Assets)
                               .AsNoTracking()
                               .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User?> GetUserWithCompleteAssetsAsync(int userId)
        {
            return await _dbSet.Include(u => u.Assets)
                                  .ThenInclude(a => a.Category)
                                .Include(u => u.Assets)
                                   .ThenInclude(a => a.Transactions)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> HasAssetsAsync(int userId)
        {
            return await _dbSet.AnyAsync(u => u.Id == userId && u.Assets.Any());
        }


        /// <param name="excludeUserId">
        /// Pass null for new users (Create). 
        /// Pass the current user's ID for existing users (Update) to avoid self-collision.
        /// </param>
        public async Task<bool> IsUserNameExistsAsync(string userName, int? excludeUserId = null)
        {
            return await _dbSet.AnyAsync(u => u.UserName == userName && (excludeUserId == null || excludeUserId != u.Id));
        }
    }
}
