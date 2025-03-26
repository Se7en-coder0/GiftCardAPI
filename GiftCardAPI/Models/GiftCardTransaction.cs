namespace GiftCardAPI.Models
{
    public class GiftCardTransaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int GiftCardId { get; set; }
        public GiftCard? GiftCard { get; set; } = null!;

        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;
        public bool Redeemed { get; set; }
    }
}
