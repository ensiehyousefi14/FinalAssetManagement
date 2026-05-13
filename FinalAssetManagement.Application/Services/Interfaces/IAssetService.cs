using FinalAssetManagement.Application.DTOs.Asset;

namespace FinalAssetManagement.Application.Services.Interfaces
{
    public interface IAssetService
    {
        Task<AssetDto?> GetAssetAsync(int assetId);
        Task<AssetDetailsDto?> GetAssetDetailsAsync(int assetId);

        // We return IEnumerable because we only promise a collection that can be iterated.
        // We do not force the result to be a specific type like List.
        // Benefits:
        // - Loose coupling: the caller is not dependent on List.
        // - Flexible implementation: it can be List, Array, yield return, EF query, etc.
        // - This is a common pattern in Repository and Service layers.
        Task<IEnumerable<AssetDto>> GetAllAssetsAsync();
        Task<IEnumerable<AssetDto>> GetAssetsByCategoryAsync(int categoryId);
        Task<IEnumerable<AssetDto>> GetAssetsByUserAsync(int userId);


        Task CreateAssetAsync(CreateAssetDto dto);
        Task FullUpdateAssetAsync(int assetId, UpdateAssetDto dto);
        Task PartialUpdateAssetAsync(int assetId, PatchAssetDto dto);
        Task RemoveAssetAsync(int assetId);
    }
}
