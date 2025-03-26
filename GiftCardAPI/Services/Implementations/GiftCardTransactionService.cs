using AutoMapper;
using GiftCardAPI.DTOs.TransactionsDTOs;
using GiftCardAPI.Models;
using GiftCardAPI.Repositories.Interfaces;
using GiftCardAPI.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GiftCardAPI.Services.Implementations
{
    public class GiftCardTransactionService : IGiftCardTransactionService
    {
        private readonly IGiftCardTransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGiftCardRepository _giftCardRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GiftCardTransactionService> _logger;

        public GiftCardTransactionService(
            IGiftCardTransactionRepository transactionRepository,
            IUserRepository userRepository,
            IGiftCardRepository giftCardRepository,
            IMapper mapper,
            ILogger<GiftCardTransactionService> logger)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _giftCardRepository = giftCardRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GiftCardTransaction> CreateTransactionAsync(int userId, int giftCardId, GiftCardTransactionDto transactionDto)
        {
            _logger.LogInformation("Creating transaction for user {UserId} and gift card {GiftCardId}", userId, giftCardId);

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found", userId);
                throw new ArgumentException("User not found");
            }

            var giftCard = await _giftCardRepository.GetByIdAsync(giftCardId);
            if (giftCard == null)
            {
                _logger.LogWarning("Gift card with ID {GiftCardId} not found", giftCardId);
                throw new ArgumentException("Gift card not found");
            }

            var transaction = _mapper.Map<GiftCardTransaction>(transactionDto);
            transaction.UserId = userId;
            transaction.GiftCardId = giftCardId;
            transaction.PurchasedAt = DateTime.UtcNow;
            transaction.Redeemed = false;

            await _transactionRepository.AddAsync(transaction);
            await _transactionRepository.SaveChangesAsync();

            _logger.LogInformation("Transaction created with ID {TransactionId}", transaction.Id);
            return transaction;
        }

        public async Task<IEnumerable<GiftCardTransaction>> GetTransactionsForUserAsync(int userId)
        {
            _logger.LogInformation("Fetching transactions for user ID {UserId}", userId);
            var transactions = (await _transactionRepository.GetTransactionsForUserAsync(userId)).ToList();

            if (!transactions.Any())
            {
                _logger.LogWarning("No transactions found for user ID {UserId}", userId);
            }
            else
            {
                _logger.LogInformation("Found {Count} transactions for user ID {UserId}", transactions.Count, userId);
            }

            return transactions;
        }

        public async Task RedeemTransactionAsync(int transactionId)
        {
            _logger.LogInformation("Redeeming transaction ID {TransactionId}", transactionId);
            var transaction = await _transactionRepository.GetByIdAsync(transactionId);

            if (transaction == null)
            {
                _logger.LogWarning("Transaction with ID {TransactionId} not found", transactionId);
                throw new ArgumentException("Transaction not found");
            }

            if (transaction.Redeemed)
            {
                _logger.LogWarning("Transaction with ID {TransactionId} is already redeemed", transactionId);
                throw new InvalidOperationException("Transaction already redeemed");
            }

            transaction.Redeemed = true;
            await _transactionRepository.SaveChangesAsync();

            _logger.LogInformation("Transaction ID {TransactionId} successfully redeemed", transactionId);
        }
    }
}
