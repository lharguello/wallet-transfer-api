using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WalletTransfer.Api.Application.DTOs.Responses;
using WalletTransfer.Api.Application.Features.Wallets.CreateWallet;
using WalletTransfer.Api.Application.Features.Wallets.DeleteWallet;
using WalletTransfer.Api.Application.Features.Wallets.GetAllWallets;
using WalletTransfer.Api.Application.Features.Wallets.GetWalletById;
using WalletTransfer.Api.Application.Features.Wallets.UpdateWallet;
using WalletTransfer.Api.Application.Wrappers;

[ApiController]
[Route("api/wallets")]
[Produces("application/json")]
public class WalletsController : ControllerBase
{
    private readonly IMediator _mediator;

    public WalletsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new wallet.
    /// </summary>
    /// <param name="request">The data for creating a new wallet.</param>
    /// <returns>The ID of the newly created wallet.</returns>
    /// <response code="201">Returns the ID of the newly created wallet.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ApiSuccessResponse<CreatedResponse>), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiInternalServerErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateWallet([FromBody] CreateWalletCommand request)
    {
        var response = await _mediator.Send(request);

        if (response.Succeeded)
        {
            return new CreatedAtRouteResult("", response);
        }
        return BadRequest(response.Message);
    }

    /// <summary>
    /// Gets a wallet by its unique ID.
    /// </summary>
    /// <param name="id" example="a1b2c3d4-e5f6-7890-1234-567890abcdef">The ID of the wallet to retrieve.</param>
    /// <returns>The wallet information.</returns>
    /// <response code="200">Returns the wallet information.</response>
    /// <response code="404">If the wallet is not found.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiSuccessResponse<WalletResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiNotFoundResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ApiInternalServerErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetWalletById([FromRoute] Guid id)
    {
        var query = new GetWalletByIdQuery() { Id = id };
        var response = await _mediator.Send(query);

        if (response.Succeeded)
        {
            return Ok(response);
        }
        return NotFound(response.Message);
    }

    /// <summary>
    /// Gets all wallets.
    /// </summary>
    /// <returns>A list of all wallets.</returns>
    /// <response code="200">Returns a list of all wallets.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet]
    [ProducesResponseType(typeof(ApiSuccessResponse<List<WalletResponseDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiInternalServerErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAllWallets()
    {
        var query = new GetAllWalletsQuery();
        var response = await _mediator.Send(query);

        if (response.Succeeded)
        {
            return Ok(response);
        }
        return StatusCode((int)HttpStatusCode.InternalServerError, response.Message);
    }

    /// <summary>
    /// Updates an existing wallet.
    /// </summary>
    /// <param name="id" example="f8e7d6c5-b4a3-2109-8765-43210fedcba9">The ID of the wallet to update.</param>
    /// <param name="command">The data for updating the wallet.</param>
    /// <returns>No content if the update was successful.</returns>
    /// <response code="204">No content if the update was successful.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="404">If the wallet to update was not found.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiNotFoundResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ApiInternalServerErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdateWallet([FromRoute] Guid id, [FromBody] UpdateWalletCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);

        return NoContent();        
    }

    /// <summary>
    /// Deletes a wallet by its unique ID.
    /// </summary>
    /// <param name="id" example="c7b6a594-f3e2-d1c0-b9a8-76543210fedc">The ID of the wallet to delete.</param>
    /// <returns>No content if the deletion was successful.</returns>
    /// <response code="204">No content if the deletion was successful.</response>
    /// <response code="404">If the wallet to delete was not found.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(ApiNotFoundResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ApiInternalServerErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeleteWallet([FromRoute] Guid id)
    {
        var command = new DeleteWalletCommand { Id = id };
        await _mediator.Send(command);

        return NoContent();
    }
}