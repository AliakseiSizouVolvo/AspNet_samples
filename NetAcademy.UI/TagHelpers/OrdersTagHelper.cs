using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetAcademy.Services.Abstractions;

namespace NetAcademy.UI.TagHelpers;


public class OrdersTagHelper : TagHelper
{
    private readonly IOrdersService _ordersService;

    [ViewContext] //inject view context from view where it will be rendered
    [HtmlAttributeNotBound] 
    public ViewContext? ViewContext { get; set; }
    
    
    public OrdersTagHelper(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagMode= TagMode.StartTagAndEndTag;
        output.TagName = "div";
        foreach (var order in _ordersService.GetOrders())
        {
            output.Content.AppendHtml($"<div>{order.OrderId} - {order.OrderData}</div>");
        }
    }
}