using AutoMapper;
using GiftCardAPI.DTOs.AddressesDTOs;
using GiftCardAPI.Models;
using GiftCardAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GiftCardAPI.Controllers
{
    [ApiController]
    [Route("api/users/{userId}/addresses")]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;
        private readonly ILogger<AddressesController> _logger;

        public AddressesController(
            IAddressService addressService,
            IMapper mapper,
            ILogger<AddressesController> logger)
        {
            _addressService = addressService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAddresses(int userId)
        {
            _logger.LogInformation("Request received: Get all addresses for user {UserId}", userId);
            var addresses = await _addressService.GetAddressesForUserAsync(userId);
            var dto = _mapper.Map<IEnumerable<AddressDto>>(addresses);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDto>> GetAddressById(int id)
        {
            _logger.LogInformation("Request received: Get address by ID {Id}", id);
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null)
            {
                _logger.LogWarning("Address with ID {Id} not found", id);
                return NotFound();
            }

            return Ok(_mapper.Map<AddressDto>(address));
        }

        [HttpPost]
        public async Task<ActionResult<AddressDto>> CreateAddress(int userId, CreateAddressDto dto)
        {
            _logger.LogInformation("Request received: Create new address for user {UserId}", userId);
            var address = _mapper.Map<Address>(dto);
            address.UserId = userId;

            var result = await _addressService.CreateAddressAsync(address);
            _logger.LogInformation("Address created successfully with ID {Id}", result.Id);

            return CreatedAtAction(nameof(GetAddressById), new { id = result.Id, userId }, _mapper.Map<AddressDto>(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int userId, int id, UpdateAddressDto dto)
        {
            if (id != dto.Id)
            {
                _logger.LogWarning("Address ID mismatch: route ID {Id} != body ID {DtoId}", id, dto.Id);
                return BadRequest("ID mismatch");
            }

            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null || address.UserId != userId)
            {
                _logger.LogWarning("Address ID {Id} not found or does not belong to user ID {UserId}", id, userId);
                return NotFound();
            }

            _mapper.Map(dto, address);
            await _addressService.UpdateAddressAsync(address);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            _logger.LogInformation("Request received: Delete address with ID {Id}", id);
            await _addressService.DeleteAddressAsync(id);
            _logger.LogInformation("Address ID {Id} deleted (if existed)", id);
            return NoContent();
        }
    }
}
