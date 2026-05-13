using FinalAssetManagement.Application.DTOs.Category;

namespace FinalAssetManagement.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto?> GetCategoryAsync(int categoryId);
        Task<CategoryDetailsDto?> GetCategoryDetailsAsync(int categoryId);
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();


        Task CreateCategoryAsync(CreateCategoryDto dto);
        Task FullUpdateCategoryAsync(int categoryId, UpdateCategoryDto dto);
        Task PartialUpdateCategoryAsync(int categoryId, PatchCategoryDto dto);
        Task RemoveCategoryAsync(int categoryId);

    }
}
