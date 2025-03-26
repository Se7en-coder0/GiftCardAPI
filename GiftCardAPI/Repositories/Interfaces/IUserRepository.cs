using GiftCardAPI.Models;

namespace GiftCardAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task DeleteAsync(User user);
        Task SaveChangesAsync();
        Task<bool> ExistsAsync(int id);
    }
}
