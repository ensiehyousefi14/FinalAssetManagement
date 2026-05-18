using FinalAssetManagement.Core.Entities;

namespace FinalAssetManagement.Contract.Repositories
{
    public interface IAssetRepository : IGenericRepository<Asset> 
    {
        Task<IEnumerable<Asset>> GetAssetsByCategoryIdAsync(int categoryId);

        Task<IEnumerable<Asset>> GetAssetsByUserIdAsync(int userId);

        //Navigation Loading for Asset

        Task<Asset?> GetAssetWithCategoryAndUserAsync(int assetId);

        Task<Asset?> GetAssetWithTransactionsAsync(int assetId);

        Task<Asset?> GetAssetWithDetailsAsync(int assetId);

    }
}
