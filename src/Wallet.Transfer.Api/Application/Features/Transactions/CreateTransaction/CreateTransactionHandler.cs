using System.Net;
using MediatR;
using WalletTransfer.Api.Application.Exceptions;
using WalletTransfer.Api.Application.Wrappers;
using WalletTransfer.Api.Core.Entities;
using WalletTransfer.Api.Core.Enums;
using WalletTransfer.Api.Core.Interfaces.Repositories;

namespace WalletTransfer.Api.Application.Features.Transactions.CreateTransaction;

public class CreateTransactionHandler(ITransactionRepository transactionRepository, IWalletRepository walletRepository) : IRequestHandler<CreateTransactionCommand, ApiSuccessResponse<CreatedResponse>>
{
    public async Task<ApiSuccessResponse<CreatedResponse>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var sourceWallet = await walletRepository.GetByIdAsync(request.SourceWalletId) ?? throw new CustomException("One or more validation failures have occurred.", (int)HttpStatusCode.BadRequest, ["The specified source wallet does not exist."]);
        var destinationWallet = await walletRepository.GetByIdAsync(request.DestinationWalletId) ?? throw new CustomException("One or more validation failures have occurred.", (int)HttpStatusCode.BadRequest, ["The specified destination wallet does not exist."]);
        if (sourceWallet != null && sourceWallet.Balance < request.Amount)
        {
            throw new CustomException("One or more validation failures have occurred.", (int)HttpStatusCode.BadRequest, ["Insufficient balance for debit transaction."]);
        }

        var debitTransaction = Transaction.Create(request.SourceWalletId, request.Amount, TransactionType.Debit);
        var creditTransaction = Transaction.Create(request.DestinationWalletId, request.Amount, TransactionType.Credit);

        await transactionRepository.AddAsync(debitTransaction);
        await transactionRepository.AddAsync(creditTransaction);

        sourceWallet!.Debit(request.Amount);
        destinationWallet.Credit(request.Amount);

        await walletRepository.UpdateAsync(sourceWallet!);
        await walletRepository.UpdateAsync(destinationWallet!);

        return new ApiSuccessResponse<CreatedResponse>(new CreatedResponse { Id = debitTransaction.Id }, "Transaction created successfully");
    }
}
