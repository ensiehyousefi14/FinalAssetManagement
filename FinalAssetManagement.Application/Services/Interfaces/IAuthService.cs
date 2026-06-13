using FinalAssetManagement.Application.DTOs.Auth;

namespace FinalAssetManagement.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }
}
