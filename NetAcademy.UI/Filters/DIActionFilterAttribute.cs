using Microsoft.AspNetCore.Mvc.Filters;
using NetAcademy.Services.Abstractions;

namespace NetAcademy.UI.Filters;

public class DIActionFilterAttribute : IActionFilter
{
    private readonly IBookService _bookService;

    public DIActionFilterAttribute(IBookService bookService)
    {
        _bookService = bookService;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {}
}