using AutoMapper;
using FinalAssetManagement.Application.Common.Interfaces;
using FinalAssetManagement.Application.DTOs.User;
using FinalAssetManagement.Application.Services.Interfaces;
using FinalAssetManagement.Contract.Repositories;
using FinalAssetManagement.Core.Entities;

namespace FinalAssetManagement.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, 
                           IMapper mapper, 
                           IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
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

        public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
        {
            if (await _unitOfWork.Users.IsUserNameExistsAsync(dto.UserName))
                throw new InvalidOperationException("UserName already exists.");

            // Hash the user's input password before saving it to the database.
            var passwordHash = _passwordHasher.Hash(dto.Password);

            User user = new User(dto.UserName, passwordHash);

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<UserDto>(user);
        }

        //------------------------------------------------------------------

        public async Task FullUpdateUserAsync(int userId, UpdateUserDto dto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException("User Not Found.");

            if (await _unitOfWork.Users.IsUserNameExistsAsync(dto.UserName, userId))
                throw new InvalidOperationException("UserName already exists.");

            user.ChangeUserName(dto.UserName);

            // Hash the user's new input password before updating it in the database.
            var passwordHash = _passwordHasher.Hash(dto.Password);
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
            {
                if (await _unitOfWork.Users.IsUserNameExistsAsync(dto.UserName, userId))
                    throw new InvalidOperationException("UserName already exists.");

                user.ChangeUserName(dto.UserName);
            }

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                // Hash the user's new input password before updating it in the database.
                var passwordHash = _passwordHasher.Hash(dto.Password);
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

            bool hasAsset = await _unitOfWork.Users.HasAssetsAsync(userId);
            if (hasAsset)
                throw new InvalidOperationException("Can not delete User because it has associated assets.");

            _unitOfWork.Users.Delete(user);
            await _unitOfWork.SaveAsync();
        }

        //------------------------------------------------------------------
    }
}
