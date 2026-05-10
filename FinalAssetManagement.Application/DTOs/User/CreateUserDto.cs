namespace FinalAssetManagement.Application.DTOs.User
{
    public class CreateUserDto // for Post
    {
        public string UserName { get; set; } = null!;

        // Plain text password sent by client. It will be hashed in the service layer before persistence.
        public string Password { get; set; } = null!;
    }
}
