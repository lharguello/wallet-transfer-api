using AutoMapper;
using MediatR;
using WalletTransfer.Api.Application.DTOs.Responses;
using WalletTransfer.Api.Application.Exceptions;
using WalletTransfer.Api.Application.Wrappers;
using WalletTransfer.Api.Core.Interfaces.Repositories;

namespace WalletTransfer.Api.Application.Features.Transactions.GetTransactions;

public class GetTransactionsHandler(ITransactionRepository transactionRepository, IWalletRepository walletRepository, IMapper mapper) : IRequestHandler<GetTransactionsQuery, ApiSuccessResponse<List<TransactionResponseDto>>>
{
    public async Task<ApiSuccessResponse<List<TransactionResponseDto>>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        var walletExists = await walletRepository.GetByIdAsync(request.WalletId) != null;

        if (!walletExists)
        {
            throw new DataNotFoundException("Wallet not found");
        }

        var transactions = await transactionRepository.GetAllByWalletIdAsync(request.WalletId);
        var transactionDtos = mapper.Map<List<TransactionResponseDto>>(transactions);

        return new ApiSuccessResponse<List<TransactionResponseDto>>(transactionDtos, $"Transactions for wallet ID '{request.WalletId}' retrieved successfully");
    }
}
