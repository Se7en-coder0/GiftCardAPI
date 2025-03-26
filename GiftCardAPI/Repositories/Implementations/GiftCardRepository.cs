using GiftCardAPI.Data;
using GiftCardAPI.Models;
using GiftCardAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GiftCardAPI.Repositories.Implementations
{
    public class GiftCardRepository : IGiftCardRepository
    {
        private readonly AppDbContext _context;

        public GiftCardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GiftCard>> GetAllAsync()
        {
            return await _context.GiftCards.ToListAsync();
        }

        public async Task<GiftCard?> GetByIdAsync(int id)
        {
            return await _context.GiftCards.FindAsync(id);
        }

        public async Task AddAsync(GiftCard giftCard)
        {
            _context.GiftCards.Add(giftCard);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(GiftCard giftCard)
        {
            _context.GiftCards.Remove(giftCard);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
