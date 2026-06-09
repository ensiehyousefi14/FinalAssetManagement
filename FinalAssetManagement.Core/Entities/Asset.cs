using FinalAssetManagement.Core.Common;
using FinalAssetManagement.Core.Enums;

namespace FinalAssetManagement.Core.Entities
{
    public class Asset : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        //--------------------------------------------------------------

        //در زمان ساخت شی حتما باید مقداردهی شوند
        public int CategoryId { get; private set; }
        public Category Category { get; private set; } = null!;


        //نیازی به مقداردهی در زمان ساخت شی نیست
        public int? UserId { get; private set; }
        public User? User { get; private set; }

        //--------------------------------------------------------------

        public List<Transaction> Transactions { get; private set; } = []; // Create a blank list (not null)

        //private readonly List<Transaction> _transactions = new();
        //public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

        //--------------------------------------------------------------

        private Asset() // for EFCore
        {

        }

        public Asset(string name, decimal price, Category category)
        {
            ChangeName(name);
            ChangePrice(price);
            ChangeCategory(category);
        }

        //--------------------------------------------------------------

        public void ChangeName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("Asset name is necessary.");
            }

            Name = newName.Trim();
        }

        public void ChangePrice(decimal newPrice)
        {
            if (newPrice < 0)
            {
                throw new ArgumentException("Asset price cannot be negative.");
            }

            Price = newPrice;
        }

        public void ChangeCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            Category = category;
            CategoryId = category.Id;
        }

        public void AssignToUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            User = user;
            UserId = user.Id;
        }

        public void RemoveUser()
        {
            User = null;
            UserId = null;
        }

        // Only the Asset aggregate can create and apply transactions.
        public Transaction AddTransaction(string description, decimal amount, TransactionType type)
        {
            var transaction = new Transaction(description, amount, this, type);

            decimal newPrice = type == TransactionType.Increase ?
                               Price + transaction.Amount :
                               Price - transaction.Amount;

            if (newPrice < 0)
                throw new InvalidOperationException("Asset Price cannot be negative.");

            Price = newPrice;

            Transactions.Add(transaction);

            return transaction;
        }
    }

    //--------------------------------------------------------------
}
