using GiftCardAPI.Models;

namespace GiftCardAPI.Repositories.Interfaces
{
    public interface IGiftCardTransactionRepository
    {
        Task<IEnumerable<GiftCardTransaction>> GetTransactionsForUserAsync(int userId);
        Task<GiftCardTransaction> GetByIdAsync(int id);
        Task AddAsync(GiftCardTransaction transaction);
        Task SaveChangesAsync();
        Task DeleteAsync(GiftCardTransaction transaction);
    }
}
