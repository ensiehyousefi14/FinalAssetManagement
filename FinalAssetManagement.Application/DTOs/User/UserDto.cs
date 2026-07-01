namespace FinalAssetManagement.Application.DTOs.User
{
    public class UserDto // for Get
    {
        // Sensitive data like PasswordHash should never be exposed in API responses

        public int Id { get; set; }

        public string UserName { get; set; } = null!;


    }
}
