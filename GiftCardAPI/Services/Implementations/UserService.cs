using AutoMapper;
using GiftCardAPI.DTOs.UsersDTOs;
using GiftCardAPI.Models;
using GiftCardAPI.Repositories.Interfaces;
using GiftCardAPI.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GiftCardAPI.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<User> CreateUserAsync(CreateUserDto userDto)
        {
            _logger.LogInformation("Creating new user...");
            var user = _mapper.Map<User>(userDto);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            _logger.LogInformation("User created with ID {UserId}", user.Id);
            return user;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            _logger.LogInformation("Fetching user by ID {UserId}", id);
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found", id);
                return null!;
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            _logger.LogInformation("Fetching all users");
            return await _userRepository.GetAllAsync();
        }

        public async Task UpdateUserAsync(int id, UpdateUserDto userDto)
        {
            _logger.LogInformation("Updating user ID {UserId}", id);
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found", id);
                return;
            }

            _mapper.Map(userDto, user);
            await _userRepository.SaveChangesAsync();

            _logger.LogInformation("User with ID {UserId} updated successfully", id);
        }

        public async Task DeleteUserAsync(int id)
        {
            _logger.LogInformation("Attempting to delete user ID {UserId}", id);
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found", id);
                return;
            }

            await _userRepository.DeleteAsync(user);
            _logger.LogInformation("User with ID {UserId} deleted successfully", id);
        }
    }
}
