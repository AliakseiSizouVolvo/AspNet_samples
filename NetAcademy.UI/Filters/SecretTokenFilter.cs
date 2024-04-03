using Microsoft.AspNetCore.Mvc.Filters;

namespace NetAcademy.UI.Filters;

public class SecretTokenAttribute : Attribute, IResultFilter
{
    private readonly string _secretToken;

    public SecretTokenAttribute(string secretToken)
    {
        _secretToken = secretToken;
    }

    public void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers.Add("SecretKey", _secretToken);
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
 
    }
}