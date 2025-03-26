using AutoMapper;
using GiftCardAPI.DTOs.AddressesDTOs;
using GiftCardAPI.DTOs.GiftCardsDTOs;
using GiftCardAPI.DTOs.TransactionsDTOs;
using GiftCardAPI.DTOs.UsersDTOs;
using GiftCardAPI.Models;

namespace GiftCardAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Addresses
            CreateMap<Address, AddressDto>();
            CreateMap<CreateAddressDto, Address>();
            CreateMap<UpdateAddressDto, Address>();
            CreateMap<AddressDto, Address>();

            // Users
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<UserDto, User>();

            // GiftCards
            CreateMap<GiftCard, GiftCardSimpleDto>();
            CreateMap<GiftCardSimpleDto, GiftCard>();
            CreateMap<CreateGiftCardDto, GiftCard>();

            // Transactions
            CreateMap<GiftCardTransaction, GiftCardTransactionDto>();
            CreateMap<CreateTransactionDto, GiftCardTransaction>();
            CreateMap<UpdateTransactionDto, GiftCardTransaction>();
        }
    }
}
