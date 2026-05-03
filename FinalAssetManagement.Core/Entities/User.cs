using FinalAssetManagement.Core.Common;

namespace FinalAssetManagement.Core.Entities
{
    public class User : BaseEntity, IAggregateRoot
    {
        public string UserName { get; private set; }
        public string PasswordHash { get; private set; }

        //--------------------------------------------------------------

        private User() //for EFCore
        {
                
        }

        public User(string userName, string passwordHash)
        {
            SetUserName(userName);
            SetPasswordHash(passwordHash);
        }

        //--------------------------------------------------------------

        public void SetUserName(string newUserName)
        {
            if (string.IsNullOrWhiteSpace(newUserName))
            {
                throw new ArgumentException("UserName is invalid");
            }

            UserName = newUserName;
        }

        public void SetPasswordHash(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
            {
                throw new ArgumentException("PasswordHash is invalid.");
            }

            PasswordHash = newPasswordHash;
        }

        //--------------------------------------------------------------

    }
}
