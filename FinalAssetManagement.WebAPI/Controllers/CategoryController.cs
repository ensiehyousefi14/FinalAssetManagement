using FinalAssetManagement.Application.DTOs.Category;
using FinalAssetManagement.Application.Services.Interfaces;
using FinalAssetManagement.WebAPI.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalAssetManagement.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //---------------------------------------------------------------------------------------------------

        [HttpGet] 
        // RouteSample = api/category

        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(new ApiResponse<IEnumerable<CategoryDto>>(categories, "All Categories Retrieved Successfully. ", true));
        }

        //---------------------------------------------------------------------------------------------------

        [HttpGet("{categoryId:int}")] 
        // RouteSample = api/category/5

        public async Task<IActionResult> GetCategory([FromRoute] int categoryId)
        {
            var category = await _categoryService.GetCategoryAsync(categoryId);

            if (category is null)
                return NotFound(new ApiResponse<CategoryDto>(null, $"Category With Id {categoryId} Not Found.", false));

            return Ok(new ApiResponse<CategoryDto>(category, "Category Retrieved Successfully.", true));
        }

        //---------------------------------------------------------------------------------------------------

        [HttpGet("details/{categoryId:int}")] 
        // RouteSample = api/category/details/5

        public async Task<IActionResult> GetCategoryDetails([FromRoute] int categoryId)
        {
            var categoryDetails = await _categoryService.GetCategoryDetailsAsync(categoryId);

            if (categoryDetails is null)
                return NotFound(new ApiResponse<CategoryDetailsDto>(null, $"CategoryDetails For Id {categoryId} Not Found.", false));

            return Ok(new ApiResponse<CategoryDetailsDto>(categoryDetails, "CategoryDetails Retrieved Successfully.", true));
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPost] 
        // RouteSample = api/category

        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
        {
            try
            {
                var createdCategory = await _categoryService.CreateCategoryAsync(dto);
                return CreatedAtAction(nameof(GetCategory), // Method for Get CreatedCategory
                                       new {categoryId = createdCategory.Id}, // parameter for GetCategory Method
                                       new ApiResponse<CategoryDto>(createdCategory, "Categroy Created Successfully.", true));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<CategoryDto>(null, ex.Message, false));
            }
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPut("{categoryId:int}")] 
        // RouteSample = api/category/5

        public async Task<IActionResult> UpdateCategory([FromRoute] int categoryId, 
                                                        [FromBody] UpdateCategoryDto dto)
        {
            try
            {
                await _categoryService.FullUpdateCategoryAsync(categoryId, dto);
                return Ok(new ApiResponse<string>(null, "Category Fully Updated Successfully.", true));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<string>(null, ex.Message, false));
            }
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPatch("{categoryId:int}")] 
        // RouteSample = api/category/5

        public async Task<IActionResult> PartialUpdateCategory([FromRoute] int categoryId, 
                                                               [FromBody] PatchCategoryDto dto)
        {
            try
            {
                await _categoryService.PartialUpdateCategoryAsync(categoryId, dto);
                return Ok(new ApiResponse<string>(null, "Category Partially Updated Successfully.", true));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<string>(null, ex.Message, false));
            }
        }

        //---------------------------------------------------------------------------------------------------

        [HttpDelete("{categoryId:int}")] 
        // RouteSample = api/category/5

        public async Task<IActionResult> RemoveCategory([FromRoute] int categoryId)
        {
            try
            {
                await _categoryService.RemoveCategoryAsync(categoryId);
                return Ok(new ApiResponse<string>(null, "Category Deleted Successfully.", true));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<string>(null, ex.Message, false));
            }
        }

        //---------------------------------------------------------------------------------------------------
    }
}
