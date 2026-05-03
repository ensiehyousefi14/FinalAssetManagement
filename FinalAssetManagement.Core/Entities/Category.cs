using FinalAssetManagement.Core.Common;

namespace FinalAssetManagement.Core.Entities
{
    public class Category : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        //--------------------------------------------------------------
        public List<Asset> Assets { get; private set; } = new(); // Create a blank list (not null)

        //--------------------------------------------------------------

        private Category() //for EFCore
        {
            
        }

        public Category(string name)
        {
            SetName(name);
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

        //--------------------------------------------------------------
    }
}
