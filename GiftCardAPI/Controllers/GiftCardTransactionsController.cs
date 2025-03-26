using AutoMapper;
using GiftCardAPI.DTOs.TransactionsDTOs;
using GiftCardAPI.Models;
using GiftCardAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GiftCardAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class GiftCardTransactionsController : ControllerBase
    {
        private readonly IGiftCardTransactionRepository _transactionRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<GiftCardTransactionsController> _logger;

        public GiftCardTransactionsController(
            IGiftCardTransactionRepository transactionRepo,
            IUserRepository userRepo,
            IMapper mapper,
            ILogger<GiftCardTransactionsController> logger)
        {
            _transactionRepo = transactionRepo;
            _userRepo = userRepo;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("users/{userId}/transactions")]
        public async Task<ActionResult<IEnumerable<GiftCardTransactionDto>>> GetTransactionsForUser(int userId)
        {
            _logger.LogInformation("Request received: Get transactions for user {UserId}", userId);

            if (!await _userRepo.ExistsAsync(userId))
            {
                _logger.LogWarning("User with ID {UserId} not found", userId);
                return NotFound();
            }

            var transactions = await _transactionRepo.GetTransactionsForUserAsync(userId);
            var dto = _mapper.Map<List<GiftCardTransactionDto>>(transactions);
            return Ok(dto);
        }

        [HttpPost("users/{userId}/transactions")]
        public async Task<ActionResult<GiftCardTransactionDto>> CreateTransaction(int userId, CreateTransactionDto dto)
        {
            _logger.LogInformation("Request received: Create transaction for user {UserId}", userId);

            if (!await _userRepo.ExistsAsync(userId))
            {
                _logger.LogWarning("User with ID {UserId} not found", userId);
                return NotFound();
            }

            var transaction = _mapper.Map<GiftCardTransaction>(dto);
            transaction.UserId = userId;
            transaction.PurchasedAt = DateTime.UtcNow;
            transaction.Redeemed = false;

            await _transactionRepo.AddAsync(transaction);
            await _transactionRepo.SaveChangesAsync();

            var fullTransaction = await _transactionRepo.GetByIdAsync(transaction.Id);
            var resultDto = _mapper.Map<GiftCardTransactionDto>(fullTransaction);

            _logger.LogInformation("Transaction created with ID {TransactionId} for user {UserId}", transaction.Id, userId);
            return CreatedAtAction(nameof(GetTransactionsForUser), new { userId }, resultDto);
        }

        [HttpPut("transactions/{id}/redeem")]
        public async Task<IActionResult> RedeemTransaction(int id)
        {
            _logger.LogInformation("Request received: Redeem transaction ID {Id}", id);

            var transaction = await _transactionRepo.GetByIdAsync(id);
            if (transaction == null)
            {
                _logger.LogWarning("Transaction with ID {Id} not found", id);
                return NotFound();
            }

            transaction.Redeemed = true;
            await _transactionRepo.SaveChangesAsync();

            _logger.LogInformation("Transaction ID {Id} marked as redeemed", id);
            return NoContent();
        }

        [HttpDelete("transactions/{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            _logger.LogWarning("Attempted DELETE request on transaction ID {Id} — not allowed", id);

            return StatusCode(StatusCodes.Status501NotImplemented, new
            {
                message = "Deleting gift card transactions is not allowed."
            });
        }
    }
}
