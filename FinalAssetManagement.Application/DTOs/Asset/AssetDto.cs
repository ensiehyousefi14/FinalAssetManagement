namespace FinalAssetManagement.Application.DTOs.Asset
{
    public class AssetDto // for Get
    {
        public int Id { get; set; }

        // null! tells the compiler to ignore the null warning.
        // We promise this property will get a value later (e.g., from API request binding).
        // string? means the value is actually allowed to be null.
        public string Name { get; set; } = null!;

        public decimal InitialPrice { get; set; }

        public int CategoryId { get; set; }

        public int UserId { get; set; }

    }
}
