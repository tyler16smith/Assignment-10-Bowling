using Assignment10_Bowling.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10_Bowling.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-info")]
    public class PaginationTagHelper : TagHelper
    {
        // private variable
        private IUrlHelperFactory _urlInfo { get; set; }

        public PaginationTagHelper(IUrlHelperFactory urh)
        {
            _urlInfo = urh;
        }

        public PageNumberingInfo PageInfo { get; set; }

        public string TeamName { get; set; }

        // dictionary for the page url number
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> KeyValuePairs { get; set; } = new Dictionary<string, object>();

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelp = _urlInfo.GetUrlHelper(ViewContext);

            TagBuilder finishedTag = new TagBuilder("div");

            for (int i = 1; i <= PageInfo.NumPages; i++) 
            {
                TagBuilder individualTag = new TagBuilder("a");

                KeyValuePairs["pageNum"] = i;
                individualTag.Attributes["href"] = urlHelp.Action("Index", KeyValuePairs);
                individualTag.InnerHtml.AppendHtml(i.ToString());

                individualTag.AddCssClass(PageClass);
                individualTag.AddCssClass(i == PageInfo.CurrentPage ? PageClassSelected : PageClassNormal);

                finishedTag.InnerHtml.AppendHtml(individualTag);
            }

            output.Content.AppendHtml(finishedTag.InnerHtml);
        }
    }
}
