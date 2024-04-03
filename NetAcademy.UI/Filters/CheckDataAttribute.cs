using Microsoft.AspNetCore.Mvc.Filters;

namespace NetAcademy.UI.Filters;

public class CheckDataAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        context.ActionArguments["id"] = 42;
        await next();
    }
}