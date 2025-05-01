using MediatR;
using WalletTransfer.Api.Application.DTOs.Responses;
using WalletTransfer.Api.Application.Wrappers;

namespace WalletTransfer.Api.Application.Features.Transactions.GetTransactions;

public class GetTransactionsQuery : IRequest<ApiSuccessResponse<List<TransactionResponseDto>>>
{
    internal Guid WalletId { get; set; }
}
