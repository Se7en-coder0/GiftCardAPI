namespace GiftCardAPI.Models
{
    public class GiftCard
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Amount { get; set; }

        public ICollection<GiftCardTransaction> Transactions { get; set; } = new List<GiftCardTransaction>();
    }
}
