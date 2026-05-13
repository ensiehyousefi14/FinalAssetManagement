using FinalAssetManagement.Application.DTOs.Asset;

namespace FinalAssetManagement.Application.DTOs.Category
{
    public class CategoryDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public IEnumerable<AssetDto> Assets { get; set; } = new List<AssetDto>();
    }
}
