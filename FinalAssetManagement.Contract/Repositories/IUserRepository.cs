using FinalAssetManagement.Core.Entities;

namespace FinalAssetManagement.Contract.Repositories
{
    // Provides queries for retrieving User entities with, or filtered by their related navigation properties.
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserWithAssetsAsync(int userId);

        Task<User?> GetUserWithCompleteAssetsAsync(int userId);

        Task<bool> IsUserNameExistsAsync(string userName, int? excludeUserId = null);

        Task<bool> HasAssetsAsync(int userId);

        Task<User?> GetByUserNameAsync(string userName);
    }
}
