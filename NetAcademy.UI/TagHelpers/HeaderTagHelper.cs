using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NetAcademy.UI.TagHelpers;

//PLEASE DON'T USE IN REAL LIFE SCENARIOS WITHOUT FULL UNDERSTANDING WHAT YOU ARE DOING
[HtmlTargetElement(Attributes = "header")]
public class HeaderTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "h2";
        output.Attributes.RemoveAll("header");
    }
}