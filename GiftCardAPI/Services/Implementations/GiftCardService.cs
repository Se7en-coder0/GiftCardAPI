using AutoMapper;
using GiftCardAPI.DTOs.GiftCardsDTOs;
using GiftCardAPI.Models;
using GiftCardAPI.Repositories.Interfaces;
using GiftCardAPI.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GiftCardAPI.Services.Implementations
{
    public class GiftCardService : IGiftCardService
    {
        private readonly IGiftCardRepository _giftCardRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GiftCardService> _logger;

        public GiftCardService(
            IGiftCardRepository giftCardRepository,
            IMapper mapper,
            ILogger<GiftCardService> logger)
        {
            _giftCardRepository = giftCardRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GiftCard> CreateGiftCardAsync(CreateGiftCardDto giftCardDto)
        {
            _logger.LogInformation("Creating new gift card...");
            var giftCard = _mapper.Map<GiftCard>(giftCardDto);
            await _giftCardRepository.AddAsync(giftCard);
            await _giftCardRepository.SaveChangesAsync();
            _logger.LogInformation("Gift card created with ID {GiftCardId}", giftCard.Id);
            return giftCard;
        }

        public async Task<GiftCard> GetGiftCardByIdAsync(int id)
        {
            _logger.LogInformation("Fetching gift card by ID {GiftCardId}", id);
            var giftCard = await _giftCardRepository.GetByIdAsync(id);
            if (giftCard == null)
            {
                _logger.LogWarning("Gift card with ID {GiftCardId} not found", id);
                return null!;
            }

            return giftCard;
        }

        public async Task<IEnumerable<GiftCard>> GetAllGiftCardsAsync()
        {
            _logger.LogInformation("Fetching all gift cards");
            return await _giftCardRepository.GetAllAsync();
        }

        public async Task UpdateGiftCardAsync(int id, CreateGiftCardDto giftCardDto)
        {
            _logger.LogInformation("Updating gift card ID {GiftCardId}", id);
            var giftCard = await _giftCardRepository.GetByIdAsync(id);
            if (giftCard == null)
            {
                _logger.LogWarning("Gift card with ID {GiftCardId} not found", id);
                return;
            }

            _mapper.Map(giftCardDto, giftCard);
            await _giftCardRepository.SaveChangesAsync();
            _logger.LogInformation("Gift card with ID {GiftCardId} updated successfully", id);
        }

        public async Task DeleteGiftCardAsync(int id)
        {
            _logger.LogInformation("Attempting to delete gift card ID {GiftCardId}", id);
            var giftCard = await _giftCardRepository.GetByIdAsync(id);
            if (giftCard == null)
            {
                _logger.LogWarning("Gift card with ID {GiftCardId} not found", id);
                return;
            }

            await _giftCardRepository.DeleteAsync(giftCard);
            _logger.LogInformation("Gift card with ID {GiftCardId} deleted successfully", id);
        }
    }
}
