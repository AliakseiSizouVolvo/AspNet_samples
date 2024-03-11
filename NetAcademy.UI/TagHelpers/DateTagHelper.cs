using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NetAcademy.UI.TagHelpers;

[HtmlTargetElement("date-tag")]
public class DateTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        
        output.TagName = "div";
        output.Content.SetContent($"Actual date is: {DateTime.Now:D}");
    }
}