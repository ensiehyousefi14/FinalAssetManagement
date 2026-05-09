using FinalAssetManagement.Core.Entities;

namespace FinalAssetManagement.Contract.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserWithAssetsAsync(int userId);

        Task<User?> GetUserWithCompleteAssetsAsync(int userId);
    }
}
