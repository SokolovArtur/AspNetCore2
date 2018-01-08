using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Tochka.Models;

namespace Tochka.TagHelpers
{
    [HtmlTargetElement("radio", Attributes = "for")]
    [HtmlTargetElement("radio", Attributes = "items")]
    public class RadioTagHelper : TagHelper
    {
        private readonly IHtmlGenerator _generator;
        
        public RadioTagHelper(IHtmlGenerator generator)
        {
            _generator = generator;
        }
        
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        
        [HtmlAttributeName("for")]
        public ModelExpression For { get; set; }
        
        [HtmlAttributeName("items")]
        public IEnumerable<RadioListItem> Items { get; set; }
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.SelfClosing;
            output.TagName = null;

            int i = 0;
            foreach (var item in Items)
            {
                i++;
                
                TagBuilder div = new TagBuilder("div");
                div.MergeAttribute("class", "form-group");
                output.PostContent.AppendHtml(div.RenderStartTag());

                string radioId = For.Name + i;
                output.PostContent.AppendHtml(
                    _generator.GenerateRadioButton(
                        ViewContext,
                        For.ModelExplorer,
                        For.Name,
                        item.Value,
                        (int) For.Model == item.Value,
                        new { Id = radioId }));
                output.PostContent.AppendHtml(
                    _generator.GenerateLabel(
                        ViewContext,
                        For.ModelExplorer,
                        radioId,
                        item.Text,
                        null));

                output.PostContent.AppendHtml(div.RenderEndTag());
            }
        }
    }
}