using System.ComponentModel.DataAnnotations;

namespace GiftCardAPI.DTOs.TransactionsDTOs
{
    public class UpdateTransactionDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public bool Redeemed { get; set; }
    }
}
