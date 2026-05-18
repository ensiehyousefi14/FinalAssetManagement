using FinalAssetManagement.Core.Enums;

namespace FinalAssetManagement.Application.DTOs.Transaction
{
    public class UpdateTransactionDto // for Put
    {
        public string Description { get; set; } = null!;

        public decimal Amount { get; set; }

        public TransactionType TransactionType { get; set; }

        public int AssetId { get; set; }
    }
}
