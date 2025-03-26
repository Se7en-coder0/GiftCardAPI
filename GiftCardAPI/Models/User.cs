using GiftCardAPI.Models;

public class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    public ICollection<GiftCardTransaction> GiftCardTransactions { get; set; } = new List<GiftCardTransaction>();
}
