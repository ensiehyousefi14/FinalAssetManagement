using AutoMapper;
using FinalAssetManagement.Application.DTOs.Category;
using FinalAssetManagement.Application.Services.Interfaces;
using FinalAssetManagement.Contract.Repositories;
using FinalAssetManagement.Core.Entities;

namespace FinalAssetManagement.Application.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //------------------------------------------------------------------

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        //------------------------------------------------------------------

        public async Task<CategoryDto?> GetCategoryAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            return _mapper.Map<CategoryDto?>(category);
        }

        //------------------------------------------------------------------

        public async Task<CategoryDetailsDto?> GetCategoryDetailsAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetCategoryWithCompleteAssetsAsync(categoryId);
            return _mapper.Map<CategoryDetailsDto?>(category);
        }

        //------------------------------------------------------------------

        public async Task CreateCategoryAsync(CreateCategoryDto dto)
        {
            Category category = new Category(dto.Name);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();
        }

        //------------------------------------------------------------------

        public async Task FullUpdateCategoryAsync(int categoryId, UpdateCategoryDto dto)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (category == null)
                throw new InvalidOperationException("Category Not Found.");

            category.ChangeName(dto.Name);

            // Update() is not required here since the entity is already tracked by EF Core.
            await _unitOfWork.SaveAsync();
        }

        //------------------------------------------------------------------

        public async Task PartialUpdateCategoryAsync(int categoryId, PatchCategoryDto dto)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (category == null)
                throw new InvalidOperationException("Category Not Found.");

            if (!string.IsNullOrWhiteSpace(dto.Name))
                category.ChangeName(dto.Name);

            await _unitOfWork.SaveAsync();

        }

        //------------------------------------------------------------------

        public async Task RemoveCategoryAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (category == null)
                throw new InvalidOperationException("Category Not Found.");

            _unitOfWork.Categories.Delete(category);
            await _unitOfWork.SaveAsync();
        }

        //------------------------------------------------------------------
    }
}
