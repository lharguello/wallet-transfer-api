using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WalletTransfer.Api.Application.DTOs.Responses;
using WalletTransfer.Api.Application.Features.Transactions.CreateTransaction;
using WalletTransfer.Api.Application.Features.Transactions.GetTransactions;
using WalletTransfer.Api.Application.Wrappers;
using WalletTransfer.Api.Presentation.Filters;

[ApiController]
[Route("api/wallets/{walletId}/transactions")]
[Produces("application/json")]
public class TransactionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new transaction for a specific wallet.
    /// </summary>
    /// <param name="walletId" example="123e4567-e89b-12d3-a456-426614174000">The ID of the wallet from which the money will be transferred.</param>
    /// <param name="request">The data for creating a new transaction.</param>
    /// <returns>The ID of the newly created transaction.</returns>
    /// <response code="201">Returns the ID of the newly created transaction.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="404">If the wallet is not found.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ApiSuccessResponse<CreatedResponse>), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiNotFoundResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ApiInternalServerErrorResponse), (int)HttpStatusCode.InternalServerError)]     
    public async Task<IActionResult> CreateTransaction([FromRoute]Guid walletId, [FromBody] CreateTransactionCommand request)
    {
        request.SourceWalletId = walletId;
        var response = await _mediator.Send(request);

        if (response.Succeeded)
        {
            return new CreatedAtRouteResult("", response);
        }
        return BadRequest(response.Message);
    }

    /// <summary>
    /// Gets all transactions for a specific wallet.
    /// </summary>
    /// <param name="walletId" example="abcdef12-3456-7890-abcd-ef1234567890">The ID of the wallet to retrieve transactions for.</param>
    /// <returns>A list of transactions for the specified wallet.</returns>
    /// <response code="200">Returns a list of transactions for the specified wallet.</response>
    /// <response code="404">If the wallet is not found.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet]
    [ProducesResponseType(typeof(ApiSuccessResponse<List<TransactionResponseDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiNotFoundResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ApiInternalServerErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetTransactions([FromRoute] Guid walletId)
    {
        var query = new GetTransactionsQuery { WalletId = walletId };
        var response = await _mediator.Send(query);

        if (response.Succeeded)
        {
            return Ok(response);
        }
        return NotFound(response.Message);
    }
}