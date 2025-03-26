using GiftCardAPI.Services.Interfaces;
using GiftCardAPI.DTOs.GiftCardsDTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace GiftCardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GiftCardsController : ControllerBase
    {
        private readonly IGiftCardService _giftCardService;
        private readonly IMapper _mapper;
        private readonly ILogger<GiftCardsController> _logger;

        public GiftCardsController(
            IGiftCardService giftCardService,
            IMapper mapper,
            ILogger<GiftCardsController> logger)
        {
            _giftCardService = giftCardService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiftCardSimpleDto>>> GetAllGiftCards()
        {
            _logger.LogInformation("Request received: Get all gift cards");
            var giftCards = await _giftCardService.GetAllGiftCardsAsync();
            var resultDto = _mapper.Map<List<GiftCardSimpleDto>>(giftCards);
            return Ok(resultDto);
        }

        [HttpPost]
        public async Task<ActionResult<GiftCardSimpleDto>> CreateGiftCard(CreateGiftCardDto giftCardDto)
        {
            _logger.LogInformation("Request received: Create new gift card");
            var giftCard = await _giftCardService.CreateGiftCardAsync(giftCardDto);
            var resultDto = _mapper.Map<GiftCardSimpleDto>(giftCard);
            _logger.LogInformation("Gift card created with ID {Id}", giftCard.Id);
            return CreatedAtAction(nameof(GetAllGiftCards), new { id = giftCard.Id }, resultDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GiftCardSimpleDto>> GetGiftCardById(int id)
        {
            _logger.LogInformation("Request received: Get gift card by ID {Id}", id);
            var giftCard = await _giftCardService.GetGiftCardByIdAsync(id);
            if (giftCard == null)
            {
                _logger.LogWarning("Gift card with ID {Id} not found", id);
                return NotFound();
            }

            var resultDto = _mapper.Map<GiftCardSimpleDto>(giftCard);
            return Ok(resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGiftCard(int id, CreateGiftCardDto giftCardDto)
        {
            _logger.LogInformation("Request received: Update gift card ID {Id}", id);
            await _giftCardService.UpdateGiftCardAsync(id, giftCardDto);
            _logger.LogInformation("Gift card ID {Id} updated successfully", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGiftCard(int id)
        {
            _logger.LogInformation("Request received: Delete gift card ID {Id}", id);
            await _giftCardService.DeleteGiftCardAsync(id);
            _logger.LogInformation("Gift card ID {Id} deleted", id);
            return NoContent();
        }
    }
}
