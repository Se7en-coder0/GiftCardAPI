namespace GiftCardAPI.DTOs.GiftCardsDTOs
{
    public class GiftCardSimpleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Amount { get; set; }
    }
}
