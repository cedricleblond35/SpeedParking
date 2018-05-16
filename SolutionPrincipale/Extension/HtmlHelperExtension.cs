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
            return MvcHtmlString.Create("<input id=\"single_cal\" name=\""+ name+"\" class=\"form-control\" placeholder=\"MM / DD / YYY\" type=\"text\" />");
        }
    }
}