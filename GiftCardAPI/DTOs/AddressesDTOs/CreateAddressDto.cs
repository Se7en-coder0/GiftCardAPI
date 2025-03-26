using System.ComponentModel.DataAnnotations;

namespace GiftCardAPI.DTOs.AddressesDTOs
{
    public class CreateAddressDto
    {
        [Required]
        public string Street { get; set; } = null!;

        [Required]
        public string City { get; set; } = null!;

        [Required]
        public string Country { get; set; } = null!;
    }
}
