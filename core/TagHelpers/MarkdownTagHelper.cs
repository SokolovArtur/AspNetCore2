using CommonMark;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Tochka.TagHelpers
{
    [HtmlTargetElement("markdown")]
    public class MarkdownTagHelper : TagHelper
    {
        public ModelExpression Value { get; set; }

        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.SelfClosing;
            output.TagName = null;

            string html = CommonMarkConverter.Convert(await GetContent(output));
            output.Content.SetHtmlContent(html ?? "");
        }

        private async Task<string> GetContent(TagHelperOutput output)
        {
            if (Value == null)
                return (await output.GetChildContentAsync()).GetContent();

            return Value.Model?.ToString();
        }
    }
}
