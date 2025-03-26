using System.ComponentModel.DataAnnotations;

namespace GiftCardAPI.DTOs.GiftCardsDTOs
{
    public class CreateGiftCardDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }
    }
}
