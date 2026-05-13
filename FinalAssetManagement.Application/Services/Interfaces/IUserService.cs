using FinalAssetManagement.Application.DTOs.User;

namespace FinalAssetManagement.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetUserAsync(int userId);
        Task<UserDetailsDto?> GetUserDetailsAsync(int userId);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        Task CreateUserAsync(CreateUserDto dto);
        Task FullUpdateUserAsync(int userId, UpdateUserDto dto);
        Task PartialUpdateUserAsync(int userId, PatchUserDto dto);
        Task RemoveUserAsync(int userId);
    }
}
