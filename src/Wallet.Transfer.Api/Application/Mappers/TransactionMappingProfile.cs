using AutoMapper;
using WalletTransfer.Api.Application.DTOs.Responses;
using WalletTransfer.Api.Core.Entities;

namespace WalletTransfer.Api.Application.Mappers;

public class TransactionMappingProfile : Profile
{
    public TransactionMappingProfile()
    {
        CreateMap<Transaction, TransactionResponseDto>();
    }
}