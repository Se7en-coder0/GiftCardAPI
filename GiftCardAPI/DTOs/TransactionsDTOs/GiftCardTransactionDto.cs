using GiftCardAPI.DTOs.GiftCardsDTOs;

namespace GiftCardAPI.DTOs.TransactionsDTOs
{
    public class GiftCardTransactionDto
    {
        public int Id { get; set; }
        public DateTime PurchasedAt { get; set; }
        public bool Redeemed { get; set; }

        public GiftCardSimpleDto GiftCard { get; set; } = null!;
    }
}
