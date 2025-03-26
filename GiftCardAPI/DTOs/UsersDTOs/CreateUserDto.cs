using System.ComponentModel.DataAnnotations;

namespace GiftCardAPI.DTOs.UsersDTOs
{
    public class CreateUserDto
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
