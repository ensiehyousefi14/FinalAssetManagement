using FinalAssetManagement.Application.DTOs.Asset;

namespace FinalAssetManagement.Application.DTOs.Transaction
{
    public class TransactionDetailsDto
    {
        public int Id { get; set; }

        public string Description { get; set; } = null!;

        public decimal Amount { get; set; }

        public string TransactionType { get; set; } = null!;

        public AssetDto Asset { get; set; } = null!;
    }
}
