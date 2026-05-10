namespace FinalAssetManagement.Application.DTOs.User
{
    public class UpdateUserDto // for Put
    {
        public string UserName { get; set; } = null!;

        // Plain text password; will be hashed in the service layer
        public string Password { get; set; } = null!; 

    }
}
