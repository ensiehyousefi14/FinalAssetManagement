using FinalAssetManagement.Application.DTOs.Asset;

namespace FinalAssetManagement.Application.DTOs.User
{
    public class UserDetailsDto
    {
        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public IEnumerable<AssetDto> Assets { get; set; } = new List<AssetDto>();
    }
}
