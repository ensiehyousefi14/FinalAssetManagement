using FinalAssetManagement.Core.Common;

namespace FinalAssetManagement.Core.Entities
{
    public class Asset : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public decimal InitialPrice { get; private set; }

        //--------------------------------------------------------------

        //در زمان ساخت شی حتما باید مقداردهی شوند
        public int CategoryId { get; private set; }
        public Category Category { get; private set; } = null!;


        //نیازی به مقداردهی در زمان ساخت شی نیست
        public int? UserId { get; private set; }
        public User? User { get; private set; }

        //--------------------------------------------------------------

        public List<Transaction> Transactions { get; private set; } = new(); // Create a blank list (not null)

        //--------------------------------------------------------------

        private Asset() // for EFCore
        {
            
        }

        public Asset(string name, decimal initialPrice, Category category)
        {
            SetName(name);
            SetPrice(initialPrice);
            SetCategory(category);
        }

        //--------------------------------------------------------------

        public void SetName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("Name is invalid.");
            }

            Name = newName;
        }

        public void SetPrice(decimal newPrice)
        {
            if (newPrice < 0)
            {
                throw new ArgumentException("Price cannot be negative.");
            }

            InitialPrice = newPrice;
        }

        public void SetCategory(Category category)
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

        //--------------------------------------------------------------
    }
}
