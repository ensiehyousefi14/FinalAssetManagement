using AutoMapper;
using FinalAssetManagement.Application.DTOs.Asset;
using FinalAssetManagement.Application.Services.Interfaces;
using FinalAssetManagement.Contract.Repositories;
using FinalAssetManagement.Core.Entities;

namespace FinalAssetManagement.Application.Services
{
    public class AssetService : IAssetService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AssetService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //------------------------------------------------------------------

        public async Task<IEnumerable<AssetDto>> GetAllAssetsAsync()
        {
            var assets = await _unitOfWork.Assets.GetAllAsync();
            return _mapper.Map<IEnumerable<AssetDto>>(assets);
        }

        //------------------------------------------------------------------

        public async Task<AssetDto?> GetAssetAsync(int assetId)
        {
            var asset = await _unitOfWork.Assets.GetAssetWithCategoryAndUserAsync(assetId);
            return _mapper.Map<AssetDto?>(asset);
        }

        //------------------------------------------------------------------

        public async Task<AssetDetailsDto?> GetAssetDetailsAsync(int assetId)
        {
            var asset = await _unitOfWork.Assets.GetAssetWithDetailsAsync(assetId);
            return _mapper.Map<AssetDetailsDto?>(asset);
        }

        //------------------------------------------------------------------

        public async Task<IEnumerable<AssetDto>> GetAssetsByCategoryAsync(int categoryId)
        {
            var assets = await _unitOfWork.Assets.GetAssetsByCategoryIdAsync(categoryId);
            return _mapper.Map<IEnumerable<AssetDto>>(assets);
        }

        //------------------------------------------------------------------

        public async Task<IEnumerable<AssetDto>> GetAssetsByUserAsync(int userId)
        {
            var assets = await _unitOfWork.Assets.GetAssetsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<AssetDto>>(assets);
        }

        //------------------------------------------------------------------

        public async Task CreateAssetAsync(CreateAssetDto dto)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(dto.CategoryId);
            if (category == null)
                throw new InvalidOperationException("Category Not Found.");

            Asset asset = new Asset(dto.Name, dto.Price, category);
            await _unitOfWork.Assets.AddAsync(asset);
            await _unitOfWork.SaveAsync();
        }

        //------------------------------------------------------------------

        public async Task FullUpdateAssetAsync(int assetId, UpdateAssetDto dto)
        {
            var asset = await _unitOfWork.Assets.GetByIdAsync(assetId);
            if (asset == null)
                throw new InvalidOperationException("Asset Not Found.");

            var category = await _unitOfWork.Categories.GetByIdAsync(dto.CategoryId);
            if (category == null)
                throw new InvalidOperationException("Category Not Found");

            asset.ChangeCategory(category);
            asset.ChangeName(dto.Name);
            asset.ChangePrice(dto.Price);

            // Update() is not required here since the entity is already tracked by EF Core.
            await _unitOfWork.SaveAsync();
        }

        //------------------------------------------------------------------

        public async Task PartialUpdateAssetAsync(int assetId, PatchAssetDto dto)
        {
            var asset = await _unitOfWork.Assets.GetByIdAsync(assetId);
            if (asset == null)
                throw new InvalidOperationException("Asset Not Found.");

            if (!string.IsNullOrWhiteSpace(dto.Name))
                asset.ChangeName(dto.Name);

            if (dto.Price.HasValue)
                asset.ChangePrice(dto.Price.Value);

            if (dto.CategoryId.HasValue)
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(dto.CategoryId.Value);
                if (category == null)
                    throw new InvalidOperationException("Category Not Found.");

                asset.ChangeCategory(category);
            }

            if (dto.UserId.HasValue)
            {
                var user = await _unitOfWork.Users.GetByIdAsync(dto.UserId.Value);
                if (user == null)
                    throw new InvalidOperationException("User Not Found.");

                asset.AssignToUser(user);
            }

            await _unitOfWork.SaveAsync();
        }

        //------------------------------------------------------------------

        public async Task RemoveAssetAsync(int assetId)
        {
            var asset = await _unitOfWork.Assets.GetByIdAsync(assetId);
            if (asset == null)
                throw new InvalidOperationException("Asset Not Found.");

            _unitOfWork.Assets.Delete(asset);
            await _unitOfWork.SaveAsync();
        }

        //------------------------------------------------------------------
    }
}
