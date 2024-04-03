using Microsoft.AspNetCore.Mvc.Filters;

namespace NetAcademy.UI.Filters;

public class DateTimeExecutionFilter : Attribute, IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
       context.HttpContext.Response.Headers.Add("Executed", DateTime.Now.ToString("G"));
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
    }
}