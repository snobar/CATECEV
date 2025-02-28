using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CATECEV.FE.Extensions
{
    public static class HtmlExtensions
    {
        public static IHtmlContent RenderYesNo(this IHtmlHelper htmlHelper, bool condition)
        {
            string output = condition ? "<span>Yes</span>" : "<span>No</span>";
            return new HtmlString(output);
        }
    }
}
