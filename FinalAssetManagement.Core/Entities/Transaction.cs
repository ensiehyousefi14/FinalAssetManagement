using FinalAssetManagement.Core.Common;
using FinalAssetManagement.Core.Enums;

namespace FinalAssetManagement.Core.Entities
{
    public class Transaction : BaseEntity
    {
        public string Description { get; private set; } = null!;
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }

        //--------------------------------------------------------------

        //در زمان ساخت شی حتما باید مقداردهی شوند
        public int AssetId { get; private set; }

        public Asset Asset { get; private set; } = null!;

        //--------------------------------------------------------------

        private Transaction() // for EFCore
        {
            
        }

        // Internal constructor so that Transaction cannot be created outside
        // the domain layer. Only the Asset aggregate should create transactions
        // to keep the domain rules and data consistency.
        internal Transaction(string description, decimal amount, Asset asset, TransactionType type)
        {
            ChangeDescription(description);
            ChangeAmount(amount);
            ChangeAsset(asset);
            ChangeType(type);
        }

        //--------------------------------------------------------------

        public void ChangeDescription(string newDescription)
        {
            if (string.IsNullOrWhiteSpace(newDescription))
            {
                throw new ArgumentException("Description is necessary.");
            }

            Description = newDescription.Trim();
        }

        public void ChangeAmount(decimal newAmount)
        {
            if (newAmount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero");
            }

            Amount = newAmount;
        }

        public void ChangeType(TransactionType transactionType)
        {
            Type = transactionType;
        }

        public void ChangeAsset(Asset newAsset)
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
