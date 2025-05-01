using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using WalletTransfer.Api.Application.Exceptions;
using WalletTransfer.Api.Application.Wrappers;

namespace WalletTransfer.Api.Presentation.Middleware;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            ApiErrorResponse responseError = new ApiErrorResponse() { Succeeded = false, Message = error?.Message };
            int statusCodeResponse = (int)HttpStatusCode.InternalServerError;

            switch (error)
            {
                case DataNotFoundException e:
                    statusCodeResponse = (int)HttpStatusCode.NotFound;
                    break;
                case BadRequestException e:
                    statusCodeResponse = (int)HttpStatusCode.BadRequest;
                    break;
                case ApiException e:
                    statusCodeResponse = (int)HttpStatusCode.BadRequest;
                    break;
                case CustomException e:
                    statusCodeResponse = e.StatusCode;
                    responseError.Errors = e.Failures;
                    break;
                case ValidationException e:
                    statusCodeResponse = (int)HttpStatusCode.BadRequest;
                    responseError.Errors = e.Errors;
                    break;
                case KeyNotFoundException e:
                    statusCodeResponse = (int)HttpStatusCode.NotFound;
                    break;
                case InternalServerErrorException e:
                    statusCodeResponse = (int)HttpStatusCode.InternalServerError;
                    break;
                default:
                    statusCodeResponse = (int)HttpStatusCode.InternalServerError;
                    if (error?.InnerException != null)
                        responseError.Errors = new List<string> { error.InnerException?.Message! };
                    break;
            }

            logger.LogError(error, "An error has occurred {message}", error?.Message);
            response.StatusCode = statusCodeResponse;
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            string? result = JsonConvert.SerializeObject(responseError, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });

            await response.WriteAsync(result);
        }
    }
}
