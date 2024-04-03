using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetAcademy.UI.Filters
{
    //ResourceFilter
    public class IEFilterAttribute : Attribute, IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, 
            ResourceExecutionDelegate next)
        {
           // var lang = context.HttpContext.Request.r
            var userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();
            if (userAgent.Contains("MSIE")
                || userAgent.Contains("Trident"))
            {
                context.Result = new ContentResult
                {
                    Content = "Internet Explorer is not supported.",
                    ContentType = "text/plain",
                    StatusCode = 400
                };
            }
            else
            {
                await next();
            }
        }
    }
}
