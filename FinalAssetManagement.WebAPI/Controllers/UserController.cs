using FinalAssetManagement.Application.DTOs.User;
using FinalAssetManagement.Application.Services.Interfaces;
using FinalAssetManagement.WebAPI.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalAssetManagement.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //---------------------------------------------------------------------------------------------------

        [HttpGet] 
        // RouteSample = api/user(ControllerName)

        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(new ApiResponse<IEnumerable<UserDto>>(users, "All Users Retrieved Successfully.", true));
        }

        //---------------------------------------------------------------------------------------------------

        [HttpGet("{userId:int}")] 
        // RouteSample = api/user/5

        public async Task<IActionResult> GetUser([FromRoute] int userId)
        {
            var user = await _userService.GetUserAsync(userId);
            if (user is null)
                return NotFound(new ApiResponse<UserDto>(null, $"User For Id {userId} Not Found.", false));

            return Ok(new ApiResponse<UserDto>(user, "User Retrieved Successfully.", true));
        }

        //---------------------------------------------------------------------------------------------------

        [HttpGet("details/{userId:int}")] 
        // RouteSample = api/user/details/5

        public async Task<IActionResult> GetUserDetails([FromRoute] int userId)
        {
            var userDetails = await _userService.GetUserDetailsAsync(userId);
            if (userDetails is null)
                return NotFound(new ApiResponse<UserDetailsDto>(null, $"UserDetails With Id {userId} Not Found.", false));

            return Ok(new ApiResponse<UserDetailsDto>(userDetails, $"UserDetails For Id {userId} Retrieved Successfully.", true));
        }

        //---------------------------------------------------------------------------------------------------

        [AllowAnonymous]
        [HttpPost] 
        // RouteSample = api/user

        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            try
            {
                var createdUser = await _userService.CreateUserAsync(dto);
                return CreatedAtAction(nameof(GetUser), // Method for Get CreatedUser
                                       new { userId = createdUser.Id}, // parameter for GetUser Method
                                       new ApiResponse<UserDto>(createdUser, $"User Created Successfully.", true));

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<UserDto>(null, ex.Message, false));
            }
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPut("{userId:int}")] 
        //RouteSample = api/user/5

        public async Task<IActionResult> UpdateUser([FromRoute] int userId, 
                                                    [FromBody] UpdateUserDto dto)
        {
            try
            {
                await _userService.FullUpdateUserAsync(userId, dto);
                return Ok(new ApiResponse<string>(null, "User Fully Updated Successfully.", true));

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<string>(null, ex.Message, false));
            }
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPatch("{userId:int}")] 
        // RouteSample = api/user/5

        public async Task<IActionResult> PartialUpdateUser([FromRoute] int userId, 
                                                           [FromBody] PatchUserDto dto)
        {
            try
            {
                await _userService.PartialUpdateUserAsync(userId, dto);
                return Ok(new ApiResponse<string>(null,"User Partially Updated Successfully.",true));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<string>(null, ex.Message, false));
            }
        }

        //---------------------------------------------------------------------------------------------------

        [HttpDelete("{userId:int}")] 
        //RouteSample = api/user/5

        public async Task<IActionResult> RemoveUser([FromRoute] int userId)
        {
            try
            {
                await _userService.RemoveUserAsync(userId);
                return Ok(new ApiResponse<string>(null, "User Deleted Successfully.", true));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<string>(null, ex.Message, false));
            }
        }

        //---------------------------------------------------------------------------------------------------
    }
}
