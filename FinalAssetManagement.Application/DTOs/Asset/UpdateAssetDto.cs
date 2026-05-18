namespace FinalAssetManagement.Application.DTOs.Asset
{
    public class UpdateAssetDto // for Put
    {
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public int UserId { get; set; }
    }
}
