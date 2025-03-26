using GiftCardAPI.Data;
using GiftCardAPI.Models;
using GiftCardAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GiftCardAPI.Repositories.Implementations
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;

        public AddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Address>> GetAddressesForUserAsync(int userId)
        {
            return await _context.Addresses
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task<Address?> GetByIdAsync(int id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Address address)
        {
            await _context.Addresses.AddAsync(address);
        }

        public void Delete(Address address)
        {
            _context.Addresses.Remove(address);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
