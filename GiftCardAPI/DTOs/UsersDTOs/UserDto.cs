using GiftCardAPI.DTOs.AddressesDTOs;
using GiftCardAPI.DTOs.TransactionsDTOs;

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<AddressDto> Addresses { get; set; } = new();
    public List<GiftCardTransactionDto> GiftCardTransactions { get; set; } = new();
}
