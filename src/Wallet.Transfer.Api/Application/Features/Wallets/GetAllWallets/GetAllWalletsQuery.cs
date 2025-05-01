using MediatR;
using WalletTransfer.Api.Application.DTOs.Responses;
using WalletTransfer.Api.Application.Wrappers;

namespace WalletTransfer.Api.Application.Features.Wallets.GetAllWallets;

public class GetAllWalletsQuery : IRequest<ApiSuccessResponse<List<WalletResponseDto>>>
{

}
