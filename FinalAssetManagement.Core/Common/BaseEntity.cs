namespace FinalAssetManagement.Core.Common
{
    public class BaseEntity
    {
        public int Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
    }
}
