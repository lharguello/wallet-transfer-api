using AutoMapper;
using MediatR;
using WalletTransfer.Api.Application.DTOs.Responses;
using WalletTransfer.Api.Application.Exceptions;
using WalletTransfer.Api.Application.Wrappers;
using WalletTransfer.Api.Core.Interfaces.Repositories;

namespace WalletTransfer.Api.Application.Features.Wallets.GetWalletById;

public class GetWalletByIdHandler(IWalletRepository walletRepository, IMapper mapper) : IRequestHandler<GetWalletByIdQuery, ApiSuccessResponse<WalletResponseDto>>
{
    public async Task<ApiSuccessResponse<WalletResponseDto>> Handle(GetWalletByIdQuery request, CancellationToken cancellationToken)
    {
        var wallet = await walletRepository.GetByIdAsync(request.Id);

        if (wallet is null)
        {
            throw new DataNotFoundException("Wallet not found");
        }

        var walletDto = mapper.Map<WalletResponseDto>(wallet);
        return new ApiSuccessResponse<WalletResponseDto>(walletDto, "Wallet retrieved successfully");
    }
}
