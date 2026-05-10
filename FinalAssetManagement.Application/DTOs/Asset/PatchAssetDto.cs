namespace FinalAssetManagement.Application.DTOs.Asset
{
    public class PatchAssetDto // for Patch
    {
        public string? Name { get; set; }

        public decimal? InitialPrice { get; set; }

        public int? CategoryId { get; set; }

        public int? UserId { get; set; }
    }
}
