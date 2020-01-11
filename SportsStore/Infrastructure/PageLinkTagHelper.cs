using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models.ViewModels;

namespace SportsStore.Infrastructure
{

    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        // Privat fält
        private IUrlHelperFactory urlHelperFactory;

        // Konstruktor
        public PageLinkTagHelper(IUrlHelperFactory helperFactory) => urlHelperFactory = helperFactory;
        
        // Sju egenskaper på rad
        // [ViewContext] respektive [HtmlAtttributeNotBound] tillhör egenskapen ViewContext
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingInfo PageModel { get; set; }
        public string PageAction { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        // Overridar den virtuella metoden Process
        // Som befinner sig i basklassen TagHelper
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
           
            TagBuilder result = new TagBuilder("div");
           
            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                
                tag.Attributes["href"] = urlHelper.Action(PageAction, new { productPage = i });
                
                if (PageClassesEnabled)
                {
                    tag.AddCssClass(PageClass);
                    
                    //Ternary Operator: Om i är lika med PageModel.Current
                    tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                }
                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}