using Microsoft.AspNetCore.Mvc.Filters;

namespace WalletTransfer.Api.Presentation.Filters;

public class DisableImplicitFromServicesValidationAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        context.ModelState.Clear();
    }
}
