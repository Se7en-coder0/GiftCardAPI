using GiftCardAPI.DTOs.GiftCardsDTOs;
using GiftCardAPI.Models;

namespace GiftCardAPI.Services.Interfaces
{
    public interface IGiftCardService
    {
        Task<GiftCard> CreateGiftCardAsync(CreateGiftCardDto giftCardDto);
        Task<GiftCard> GetGiftCardByIdAsync(int id);
        Task<IEnumerable<GiftCard>> GetAllGiftCardsAsync();
        Task UpdateGiftCardAsync(int id, CreateGiftCardDto giftCardDto);
        Task DeleteGiftCardAsync(int id);
    }
}
