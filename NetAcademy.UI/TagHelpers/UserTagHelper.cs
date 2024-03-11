using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NetAcademy.UI.TagHelpers;

public class UserTagHelper : TagHelper
{
    //just FYI - with complex object same logic
    public string? UserEmail { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";

        if (string.IsNullOrWhiteSpace(UserEmail))
        {
            output.Content.SetHtmlContent("<div> You need to be logged in to see that content</div>");
        }
        else
        {
            output.Content.SetContent($"Hello {UserEmail}");
        }
    }
}