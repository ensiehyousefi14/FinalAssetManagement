using FinalAssetManagement.Core.Enums;

namespace FinalAssetManagement.Application.DTOs.Transaction
{
    public class PatchTransactionDto // for Patch
    {
        public string? Description { get; set; }

        public decimal? Amount { get; set; }

        public TransactionType? TransactionType { get; set; }

        public int? AssetId { get; set; }
    }
}
