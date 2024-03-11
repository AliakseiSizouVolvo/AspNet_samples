using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NetAcademy.UI.TagHelpers;

public class SummaryTagHelper : TagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.SetAttribute("class", "my-own-summary-class");
        var childContent = await output.GetChildContentAsync();
        var content = $"<h3>Date and time summary:</h3> {childContent.GetContent()}";
        //pre
        output.PreContent.SetContent("Hello there");
        
        output.PostContent.SetHtmlContent("<h5>Hello there again</h5>");

        output.Content.SetHtmlContent(content);
    }
}