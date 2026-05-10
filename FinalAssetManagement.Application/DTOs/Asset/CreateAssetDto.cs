namespace FinalAssetManagement.Application.DTOs.Asset
{
    public class CreateAssetDto // for Post
    {
        public string Name { get; set; } = null!;

        public decimal InitialPrice { get; set; }

        public int CategoryId { get; set; }

        public int UserId { get; set; }

    }
}
