using GiftCardAPI.DTOs.UsersDTOs;

namespace GiftCardAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(CreateUserDto userDto);
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task UpdateUserAsync(int id, UpdateUserDto userDto);
        Task DeleteUserAsync(int id);
    }
}
