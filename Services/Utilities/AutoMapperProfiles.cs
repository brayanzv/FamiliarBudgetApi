using AutoMapper;
using Data.DTOs;
using Data.Entity;


namespace Services.Utilities
{
    internal class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<UserCreationDto, User>();
            CreateMap<User,UserResponseDto>();
            CreateMap<User, AuthenticationResponse>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<UserPasswordDto, User>();

            CreateMap<WalletCreationDto, Wallet>();
            CreateMap<Wallet, WalletResponseDto>();
            CreateMap<WalletUpdateDto, Wallet>();

            CreateMap<TransactionCreationDto, Transaction>();
            CreateMap<Transaction, TransactionResponseDto>();
            CreateMap<TransactionUpdateDto, TransactionResponseDto>();
            CreateMap<TransactionUpdateDto, Transaction>();
        }
    }
}
