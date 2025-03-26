using GiftCardAPI.Models;

public interface IAddressService
{
    Task<IEnumerable<Address>> GetAddressesForUserAsync(int userId);
    Task<Address> GetAddressByIdAsync(int id);
    Task<Address> CreateAddressAsync(Address address);
    Task UpdateAddressAsync(Address address);
    Task DeleteAddressAsync(int id);
}
