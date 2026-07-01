using FinalAssetManagement.Application.DTOs.Asset;
using FinalAssetManagement.Application.Services.Interfaces;
using FinalAssetManagement.WebAPI.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalAssetManagement.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {

        private readonly IAssetService _assetService;
        public AssetController(IAssetService assetService)
        {
            این یک خطای عمدی است برای 2تست CI;

            _assetService = assetService;
        }

        //---------------------------------------------------------------------------------------------------

        [HttpGet] 
        // RouteSample = api/asset(ControllerName)

        public async Task<IActionResult> GetAllAssets()
        {
            var assets = await _assetService.GetAllAssetsAsync();
            return Ok(new ApiResponse<IEnumerable<AssetDto>>(assets, "Assets Retrieved Succesfully.", true));
        }

        //---------------------------------------------------------------------------------------------------

        [HttpGet("{assetId}")] 
        // RouteSample = api/asset/5

        public async Task<IActionResult> GetAsset([FromRoute] int assetId)
        {
            var asset = await _assetService.GetAssetAsync(assetId);

            if (asset is null)
                return NotFound(new ApiResponse<AssetDto>(null, $"Asset With Id {assetId} Not Found.", false));

            return Ok(new ApiResponse<AssetDto>(asset, "Asset Retrieved Successfully.", true));
        }

        //---------------------------------------------------------------------------------------------------

        [HttpGet("details/{assetId}")] 
        // RouteSample = api/asset/details/5

        public async Task<IActionResult> GetAssetDetails([FromRoute] int assetId)
        {
            var assetDetails = await _assetService.GetAssetDetailsAsync(assetId);

            if (assetDetails is null)
                return NotFound(new ApiResponse<AssetDetailsDto>(null, $"AssetDetails For Id {assetId} Not Found.", false));

            return Ok(new ApiResponse<AssetDetailsDto>(assetDetails, "AssetDetails Retrieved Successfully.", true));
        }

        //---------------------------------------------------------------------------------------------------

        [HttpGet("category/{categoryId}")] 
        // RouteSample = api/asset/category/3

        public async Task<IActionResult> GetAssetsByCategory([FromRoute] int categoryId)
        {
            var assets = await _assetService.GetAssetsByCategoryAsync(categoryId);
            return Ok(new ApiResponse<IEnumerable<AssetDto>>(assets, "Assets for This Category Retrieved.", true));
        }

        //---------------------------------------------------------------------------------------------------

        [HttpGet("user/{userId}")] 
        // RouteSample = api/asset/user/4

        public async Task<IActionResult> GetAssetsByUser([FromRoute] int userId)
        {
            var assets = await _assetService.GetAssetsByUserAsync(userId);
            return Ok(new ApiResponse<IEnumerable<AssetDto>>(assets, "Assets for This User Retrieved.", true));
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPost] 
        // RouteSample = api/asset

        public async Task<IActionResult> CreateAsset([FromBody] CreateAssetDto dto)
        {
            try
            {
                var createdAsset = await _assetService.CreateAssetAsync(dto);
                return CreatedAtAction(nameof(GetAsset),  // Method for Get CreatedAsset
                                       new { assetId = createdAsset.Id}, // parameter for GetAsset Method
                                       new ApiResponse<AssetDto>(createdAsset, "Asset Created Successfully.", true));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<AssetDto>(null, ex.Message, false));
            }
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPut("{assetId}")] 
        // RouteSample = api/asset/5

        public async Task<IActionResult> UpdateAsset([FromRoute] int assetId, 
                                                     [FromBody] UpdateAssetDto dto)
        {
            try
            {
                await _assetService.FullUpdateAssetAsync(assetId, dto);
                return Ok(new ApiResponse<string>(null, "Asset Fully Updated Successfully", true));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<string>(null, ex.Message, false));
            }
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPatch("{assetId}")] 
        // RouteSample = api/asset/5

        public async Task<IActionResult> PartialUpdateAsset([FromRoute] int assetId, 
                                                            [FromBody] PatchAssetDto dto)
        {
            try
            {
                await _assetService.PartialUpdateAssetAsync(assetId, dto);
                return Ok(new ApiResponse<string>(null, "Asset Partially Updated Successfully.", true));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<string>(null, ex.Message, false));
            }
        }

        //---------------------------------------------------------------------------------------------------

        [HttpDelete("{assetId}")] 
        // routeSample = api/asset/5

        public async Task<IActionResult> RemoveAsset([FromRoute] int assetId)
        {
            try
            {
                await _assetService.RemoveAssetAsync(assetId);
                return Ok(new ApiResponse<string>(null, "Asset Deleted Successfully.", true));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<string>(null, ex.Message, false));
            }
        }

        //---------------------------------------------------------------------------------------------------
    }
}
