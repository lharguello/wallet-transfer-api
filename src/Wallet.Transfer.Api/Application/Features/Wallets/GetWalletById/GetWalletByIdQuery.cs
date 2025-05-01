using MediatR;
using WalletTransfer.Api.Application.DTOs.Responses;
using WalletTransfer.Api.Application.Wrappers;

namespace WalletTransfer.Api.Application.Features.Wallets.GetWalletById;

public class GetWalletByIdQuery : IRequest<ApiSuccessResponse<WalletResponseDto>>
{
    internal Guid Id { get; set; }
}