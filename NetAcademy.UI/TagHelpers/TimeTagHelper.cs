using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NetAcademy.UI.TagHelpers;

public class TimeTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Content.SetContent($"Actual time is: {DateTime.Now:T}");
    }
}