using FinalAssetManagement.Core.Entities;

namespace FinalAssetManagement.Contract.Repositories
{
    // Provides queries for retrieving Category entities with, or filtered by their related navigation properties.
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        //Navigation Loading for Category
        Task<Category?> GetCategoryWithAssetsAsync(int categoryId);

        Task<Category?> GetCategoryWithCompleteAssetsAsync(int categoryId);

        Task<bool> HasAssetsAsync(int categoryId);
    }
}
