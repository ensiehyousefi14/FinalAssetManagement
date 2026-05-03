using FinalAssetManagement.Core.Common;

namespace FinalAssetManagement.Core.Entities
{
    public class Transaction : BaseEntity
    {
        public string Description { get; private set; }
        public decimal Amount { get; private set; }

        //--------------------------------------------------------------

        //در زمان ساخت شی حتما باید مقداردهی شوند
        public int AssetId { get; private set; }

        public Asset Asset { get; private set; } = null!;

        //--------------------------------------------------------------

        private Transaction() // for EFCore
        {
            
        }

        public Transaction(string description, decimal amount, Asset asset)
        {
            SetDescription(description);
            SetAmount(amount);
            SetAsset(asset);
        }

        //--------------------------------------------------------------

        public void SetDescription(string newDescription)
        {
            if (string.IsNullOrWhiteSpace(newDescription))
            {
                throw new ArgumentException("Description is invalid.");
            }

            Description = newDescription;
        }

        public void SetAmount(decimal newAmount)
        {
            if (newAmount < 0)
            {
                throw new ArgumentException("Amount cannot be negative.");
            }

            Amount = newAmount;
        }

        public void SetAsset(Asset newAsset)
        {
            if (newAsset == null)
            {
                throw new ArgumentNullException(nameof(newAsset));
            }

            Asset = newAsset;
            AssetId = newAsset.Id;
        }

        //--------------------------------------------------------------
    }
}
