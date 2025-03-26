using GiftCardAPI.Models;

namespace GiftCardAPI.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAddressesForUserAsync(int userId);
        Task<Address?> GetByIdAsync(int id);
        Task AddAsync(Address address);
        void Delete(Address address);
        Task SaveChangesAsync();
    }
}
