using System.ComponentModel.DataAnnotations;

namespace GiftCardAPI.DTOs.TransactionsDTOs
{
    public class CreateTransactionDto
    {
        [Required]
        public int GiftCardId { get; set; }
    }
}
