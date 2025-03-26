using GiftCardAPI.Data;
using GiftCardAPI.Models;
using GiftCardAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GiftCardAPI.Repositories.Implementations
{
    public class GiftCardTransactionRepository : IGiftCardTransactionRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<GiftCardTransactionRepository> _logger;

        public GiftCardTransactionRepository(AppDbContext context, ILogger<GiftCardTransactionRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<GiftCardTransaction>> GetTransactionsForUserAsync(int userId)
        {
            _logger.LogInformation("Fetching transactions for user ID {UserId}", userId);

            var transactions = await _context.GiftCardTransactions
                .Where(t => t.UserId == userId)
                .Include(t => t.GiftCard)
                .ToListAsync();

            _logger.LogInformation("{TransactionCount} transactions retrieved for user ID {UserId}", transactions.Count, userId);

            return transactions;
        }

        public async Task<GiftCardTransaction> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching transaction with ID {TransactionId}", id);

            var transaction = await _context.GiftCardTransactions
                .Include(t => t.GiftCard)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
            {
                _logger.LogWarning("Gift card transaction with ID {TransactionId} not found", id);
                return null!;
            }

            _logger.LogInformation("Transaction with ID {TransactionId} successfully retrieved", id);
            return transaction;
        }

        public async Task AddAsync(GiftCardTransaction transaction)
        {
            await _context.GiftCardTransactions.AddAsync(transaction);

            var giftCardAmount = transaction.GiftCard?.Amount;

            if (giftCardAmount.HasValue)
            {
                _logger.LogInformation("New gift card transaction added for user ID {UserId}, GiftCard ID {GiftCardId}, Amount {Amount}",
                    transaction.UserId, transaction.GiftCardId, giftCardAmount.Value);
            }
            else
            {
                _logger.LogInformation("New gift card transaction added for user ID {UserId}, GiftCard ID {GiftCardId}, Amount unknown (GiftCard is null)",
                    transaction.UserId, transaction.GiftCardId);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            _logger.LogInformation("Database changes saved successfully");
        }

        public async Task DeleteAsync(GiftCardTransaction transaction)
        {
            _context.GiftCardTransactions.Remove(transaction);
            _logger.LogInformation("Gift card transaction with ID {TransactionId} marked for deletion", transaction.Id);
        }
    }
}
