using FinalAssetManagement.Application.DTOs.Auth;
using FinalAssetManagement.Application.Services.Interfaces;
using FinalAssetManagement.Contract.Repositories;
using FinalAssetManagement.Core.Common;
using FinalAssetManagement.Core.Entities;

namespace FinalAssetManagement.Application.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        //------------------------------------------------------------------

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _unitOfWork.Users.GetByUserNameAsync(dto.UserName);
            if (user is null)
                throw new InvalidOperationException("Invalid UserName or Password.");

            var isPasswordValid = PasswordHasher.Verify(dto.Password, user.PasswordHash);
            if (!isPasswordValid)
                throw new InvalidOperationException("Invalid UserName or Password.");

            var token = _jwtTokenGenerator.GenerateToken(user.Id, user.UserName);

            return new AuthResponseDto { UserName = dto.UserName, Token = token };
        }

        //------------------------------------------------------------------

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {

            var isUserNameExists = await _unitOfWork.Users.IsUserNameExistsAsync(dto.UserName);
            if (isUserNameExists)
                throw new InvalidOperationException("UserName already exists.");

            var passwordHash = PasswordHasher.Hash(dto.Password);

            var user = new User(dto.UserName, passwordHash);

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();

            var token = _jwtTokenGenerator.GenerateToken(user.Id, user.UserName);

            return new AuthResponseDto { UserName = dto.UserName, Token = token };
        }

        //------------------------------------------------------------------
    }
}
