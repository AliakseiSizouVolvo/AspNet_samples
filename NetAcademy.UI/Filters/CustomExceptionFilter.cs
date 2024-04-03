using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetAcademy.UI.Filters;

public class CustomExceptionAttribute : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var actionName = context.ActionDescriptor.DisplayName;
        var stackTrace = context.Exception.StackTrace;
        var message = context.Exception.Message;

        context.Result = new ContentResult()
        {
            Content =
                $"Action {actionName} exception has been handled: {Environment.NewLine} {message} {Environment.NewLine} Stack Trace: {stackTrace} "
        };
    }
}