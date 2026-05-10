namespace FinalAssetManagement.Application.DTOs.Transaction
{
    public class TransactionDto // for Get
    {
        public int Id { get; set; }

        public string Description { get; set; } = null!;

        public decimal Amount { get; set; }

        public int AssetId { get; set; }
    }
}
