using FinalAssetManagement.Core.Common;

namespace FinalAssetManagement.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }

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
