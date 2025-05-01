using AutoMapper;
using MediatR;
using WalletTransfer.Api.Application.DTOs.Responses;
using WalletTransfer.Api.Application.Wrappers;
using WalletTransfer.Api.Core.Interfaces.Repositories;

namespace WalletTransfer.Api.Application.Features.Wallets.GetAllWallets;

public class GetAllWalletsHandler(IWalletRepository walletRepository, IMapper mapper) : IRequestHandler<GetAllWalletsQuery, ApiSuccessResponse<List<WalletResponseDto>>>
{
    public async Task<ApiSuccessResponse<List<WalletResponseDto>>> Handle(GetAllWalletsQuery request, CancellationToken cancellationToken)
    {
        var wallets = await walletRepository.GetAllAsync();
        var walletDtos = mapper.Map<List<WalletResponseDto>>(wallets);

        return new ApiSuccessResponse<List<WalletResponseDto>>(walletDtos, "Wallets retrieved successfully");
    }
}
