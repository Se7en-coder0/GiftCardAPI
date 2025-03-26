using GiftCardAPI.Data;
using GiftCardAPI.Models;
using GiftCardAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GiftCardAPI.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Addresses)
                .Include(u => u.GiftCardTransactions)
                    .ThenInclude(t => t.GiftCard)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Addresses)
                .Include(u => u.GiftCardTransactions)
                    .ThenInclude(t => t.GiftCard)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }
    }
}
