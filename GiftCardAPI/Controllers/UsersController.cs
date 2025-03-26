using GiftCardAPI.Services.Interfaces;
using GiftCardAPI.DTOs.UsersDTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace GiftCardAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            ILogger<UsersController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto userDto)
        {
            _logger.LogInformation("Creating new user...");
            var user = await _userService.CreateUserAsync(userDto);
            var resultDto = _mapper.Map<UserDto>(user);
            _logger.LogInformation("User created with ID {Id}", user.Id);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, resultDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            _logger.LogInformation("Fetching user by ID {Id}", id);
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID {Id} not found", id);
                return NotFound();
            }

            var resultDto = _mapper.Map<UserDto>(user);
            return Ok(resultDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            _logger.LogInformation("Fetching all users...");
            var users = await _userService.GetAllUsersAsync();
            var resultDto = _mapper.Map<List<UserDto>>(users);
            return Ok(resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDto userDto)
        {
            _logger.LogInformation("Updating user ID {Id}", id);
            await _userService.UpdateUserAsync(id, userDto);
            _logger.LogInformation("User ID {Id} updated", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation("Deleting user ID {Id}", id);
            await _userService.DeleteUserAsync(id);
            _logger.LogInformation("User ID {Id} deleted", id);
            return NoContent();
        }
    }
}
