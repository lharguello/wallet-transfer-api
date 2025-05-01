using Microsoft.AspNetCore.Mvc;
using WalletTransfer.Api.Application.Wrappers;

namespace WalletTransfer.Api.Application.Behaviors;

public class BehaviorBadRequest
{
    public static void ParseModelErrors(ApiBehaviorOptions options)
    {
        options.InvalidModelStateResponseFactory = (context) =>
        {
            ApiErrorResponse responseError = new ApiErrorResponse { Succeeded = false, Message = "One or more validation failures have occurred", Errors = null };
            if (!context.ModelState.IsValid)
            {
                responseError.Errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToList();
            }
            return new BadRequestObjectResult(responseError);
        };
    }
}
