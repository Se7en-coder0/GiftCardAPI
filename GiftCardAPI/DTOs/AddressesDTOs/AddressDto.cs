namespace GiftCardAPI.DTOs.AddressesDTOs
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
