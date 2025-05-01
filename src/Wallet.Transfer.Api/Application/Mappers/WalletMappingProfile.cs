using AutoMapper;
using WalletTransfer.Api.Application.DTOs.Responses;
using WalletTransfer.Api.Core.Entities;

namespace WalletTransfer.Api.Application.Mappers;

public class WalletMappingProfile : Profile
{
    public WalletMappingProfile()
    {
        CreateMap<Wallet, WalletResponseDto>();
    }
}
