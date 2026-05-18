using AutoMapper;
using FinalAssetManagement.Application.DTOs.User;
using FinalAssetManagement.Application.Services.Interfaces;
using FinalAssetManagement.Contract.Repositories;
using FinalAssetManagement.Core.Common;
using FinalAssetManagement.Core.Entities;

namespace FinalAssetManagement.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //------------------------------------------------------------------

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        //------------------------------------------------------------------

        public async Task<UserDto?> GetUserAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            return _mapper.Map<UserDto?>(user);
        }

        //------------------------------------------------------------------

        public async Task<UserDetailsDto?> GetUserDetailsAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetUserWithCompleteAssetsAsync(userId);
            return _mapper.Map<UserDetailsDto?>(user);
        }

        //------------------------------------------------------------------

        public async Task CreateUserAsync(CreateUserDto dto)
        {
            // Hash the user's input password before saving it to the database.
            var passwordHash = PasswordHasher.Hash(dto.Password);

            User user = new User(dto.UserName, passwordHash);

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();
        }

        //------------------------------------------------------------------

        public async Task FullUpdateUserAsync(int userId, UpdateUserDto dto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException("User Not Found.");

            user.ChangeUserName(dto.UserName);

            // Hash the user's new input password before updating it in the database.
            var passwordHash = PasswordHasher.Hash(dto.Password);
            user.ChangePasswordHash(passwordHash);

            // Update() is not required here since the entity is already tracked by EF Core.
            await _unitOfWork.SaveAsync();
        }

        //------------------------------------------------------------------

        public async Task PartialUpdateUserAsync(int userId, PatchUserDto dto)
        {
            //Entity was tracked
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException("User Not Found.");

            if (!string.IsNullOrWhiteSpace(dto.UserName))
                user.ChangeUserName(dto.UserName);

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                // Hash the user's new input password before updating it in the database.
                var passwordHash = PasswordHasher.Hash(dto.Password);
                user.ChangePasswordHash(passwordHash);
            }

            await _unitOfWork.SaveAsync();
        }

        //------------------------------------------------------------------

        public async Task RemoveUserAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException("User Not Found.");

            _unitOfWork.Users.Delete(user);
            await _unitOfWork.SaveAsync();
        }

        //------------------------------------------------------------------
    }
}
