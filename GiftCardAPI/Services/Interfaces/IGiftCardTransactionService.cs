using GiftCardAPI.DTOs.TransactionsDTOs;
using GiftCardAPI.Models;

namespace GiftCardAPI.Services.Interfaces
{
    public interface IGiftCardTransactionService
    {
        Task<GiftCardTransaction> CreateTransactionAsync(int userId, int giftCardId, GiftCardTransactionDto transactionDto);
        Task<IEnumerable<GiftCardTransaction>> GetTransactionsForUserAsync(int userId);
        Task RedeemTransactionAsync(int transactionId);
    }
}
