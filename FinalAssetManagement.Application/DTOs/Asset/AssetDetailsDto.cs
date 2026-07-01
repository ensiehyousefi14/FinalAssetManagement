using FinalAssetManagement.Application.DTOs.Category;
using FinalAssetManagement.Application.DTOs.Transaction;
using FinalAssetManagement.Application.DTOs.User;

namespace FinalAssetManagement.Application.DTOs.Asset
{
    public class AssetDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public CategoryDto Category { get; set; } = null!;
        public UserDto? User { get; set; }
        public IEnumerable<TransactionDto> Transactions { get; set; } =
                                                        new List<TransactionDto>();

    }
}
