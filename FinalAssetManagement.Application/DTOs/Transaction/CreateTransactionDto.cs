namespace FinalAssetManagement.Application.DTOs.Transaction
{
    public class CreateTransactionDto // for Post
    {
        public string Description { get; set; } = null!;

        public decimal Amount { get; set; }

        public int AssetId { get; set; }
    }
}
