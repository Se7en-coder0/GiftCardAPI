using GiftCardAPI.Models;
using GiftCardAPI.Repositories.Interfaces;
using GiftCardAPI.Services.Interfaces;
using Microsoft.Extensions.Logging;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly ILogger<AddressService> _logger;

    public AddressService(IAddressRepository addressRepository, ILogger<AddressService> logger)
    {
        _addressRepository = addressRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Address>> GetAddressesForUserAsync(int userId)
    {
        _logger.LogInformation("Fetching addresses for user ID {UserId}", userId);
        return await _addressRepository.GetAddressesForUserAsync(userId);
    }

    public async Task<Address> GetAddressByIdAsync(int id)
    {
        _logger.LogInformation("Fetching address by ID {Id}", id);
        return await _addressRepository.GetByIdAsync(id);
    }

    public async Task<Address> CreateAddressAsync(Address address)
    {
        _logger.LogInformation("Creating new address for user ID {UserId}", address.UserId);
        await _addressRepository.AddAsync(address);
        await _addressRepository.SaveChangesAsync();
        _logger.LogInformation("Address created with ID {Id}", address.Id);
        return address;
    }

    public async Task UpdateAddressAsync(Address address)
    {
        _logger.LogInformation("Updating address ID {Id}", address.Id);
        await _addressRepository.SaveChangesAsync();
        _logger.LogInformation("Address ID {Id} updated successfully", address.Id);
    }

    public async Task DeleteAddressAsync(int id)
    {
        _logger.LogInformation("Attempting to delete address ID {Id}", id);
        var address = await _addressRepository.GetByIdAsync(id);
        if (address != null)
        {
            _addressRepository.Delete(address);
            await _addressRepository.SaveChangesAsync();
            _logger.LogInformation("Address ID {Id} deleted", id);
        }
        else
        {
            _logger.LogWarning("Attempted to delete non-existent address ID {Id}", id);
        }
    }
}
