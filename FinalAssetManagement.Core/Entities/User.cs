using FinalAssetManagement.Core.Common;

namespace FinalAssetManagement.Core.Entities
{
    public class User : BaseEntity, IAggregateRoot
    {
        public string UserName { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;

        //--------------------------------------------------------------
        public List<Asset> Assets { get; private set; } = []; // Create a blank list (not null)

        //--------------------------------------------------------------

        private User() //for EFCore
        {

        }

        public User(string userName, string passwordHash)
        {
            ChangeUserName(userName);
            ChangePasswordHash(passwordHash);
        }

        //--------------------------------------------------------------

        public void ChangeUserName(string newUserName)
        {
            if (string.IsNullOrWhiteSpace(newUserName))
            {
                throw new ArgumentException("UserName is necessary");
            }

            UserName = newUserName.Trim();
        }

        public void ChangePasswordHash(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
            {
                throw new ArgumentException("PasswordHash is necessary.");
            }

            PasswordHash = newPasswordHash.Trim();
        }

        //--------------------------------------------------------------

    }
}
