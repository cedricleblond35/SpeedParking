using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace SolutionPrincipale.Extension
{
    public static class HtmlHelperExtension
    {
        public static MvcHtmlString CustomerDate<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, string name)
        {
            return MvcHtmlString.Create("<input id=\"single_cal\" name=\"" + name + "\" class=\"form-control\" placeholder=\"MM / DD / YYY\" type=\"text\" />");
        }

        public static MvcHtmlString CustomIcone<TModel>(this HtmlHelper<TModel> html, string action, string glyphicon)
        {
            return MvcHtmlString.Create("<a href='@Url.Action(\""+action+"\", new {id=item.Id})'><span class=\"glyphicon glyphicon-"+glyphicon+"\" aria-hidden=\"true\"></span></a>");
        }
    }
}