using FinalAssetManagement.Core.Common;

namespace FinalAssetManagement.Core.Entities
{
    public class Category : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        //--------------------------------------------------------------
        public List<Asset> Assets { get; private set; } = new(); // Create a blank list (not null)

        //private readonly List<Asset> _assets = new();
        //public IReadOnlyCollection<Asset> Assets => _assets.AsReadOnly();

        //--------------------------------------------------------------

        private Category() //for EFCore
        {
            
        }

        public Category(string name)
        {
            ChangeName(name);
        }

        //--------------------------------------------------------------

        public void ChangeName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("Category Name is necessary.");
            }

            Name = newName.Trim();
        }

        //--------------------------------------------------------------
    }
}
