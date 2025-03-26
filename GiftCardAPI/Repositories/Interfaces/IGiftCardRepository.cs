using GiftCardAPI.Models;

namespace GiftCardAPI.Repositories.Interfaces
{
    public interface IGiftCardRepository
    {
        Task<IEnumerable<GiftCard>> GetAllAsync();
        Task<GiftCard?> GetByIdAsync(int id);
        Task AddAsync(GiftCard giftCard);
        Task DeleteAsync(GiftCard giftCard);
        Task SaveChangesAsync();
    }
}
