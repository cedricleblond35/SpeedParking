using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SolutionPrincipale.Extension
{
    public static class HtmlHelperExtension
    {
        private static string colmd10 = "<div class=\"col-md-10\">";
        public static MvcHtmlString CustomerDate<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string name)
        {
            return MvcHtmlString.Create("<input id=\"single_cal\" name=\"" +
                name +
                "\" class=\"form-control\" placeholder=\"MM / DD / YYY\" type=\"text\" />");
        }

        public static MvcHtmlString CustomIcone<TModel>(this HtmlHelper<TModel> html, string action, string glyphicon)
        {
            return MvcHtmlString.Create("<a href='@Url.Action(\"" + action + "\", new {id=item.Id})'><span class=\"glyphicon glyphicon-" + glyphicon + "\" aria-hidden=\"true\"></span></a>");
        }
        public static MvcHtmlString CustomEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string type)
        {
            return MvcHtmlString.Create("<div class=\"form-group\">" +
                html.LabelFor(expression, htmlAttributes: new { @class = "control-label col-md-2" }) +
                colmd10 +
                html.EditorFor(expression, new { htmlAttributes = new { @class = "form-control", @type = "" + type + "" } }) +
                html.ValidationMessageFor(expression, "", new { @class = "text-danger" }) +
                "</div></div>");
        }
        public static MvcHtmlString CustomButtonSubmit<TModel>(this HtmlHelper<TModel> html, string label)
        {
            return MvcHtmlString.Create("<div class=\"form-group\"><div class=\"col-md-offset-2 col-md-10\">" +
                "<input type = \"submit\" value=\"" + label + "\" class=\"btn btn-default\"/></div></div>");
        }
        public static MvcHtmlString CustomDropdownListFor<TModel, TValueLabel, TValueSelected>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValueLabel>> expression, Expression<Func<TModel, TValueSelected>> expressionSelected, IEnumerable<SelectListItem> list, bool multiple)
        {
            MvcHtmlString chaine;
            if (multiple)
            {
                chaine = html.DropDownListFor(expressionSelected, list, new { htmlAttributes = new { @class = "form-control" } });
            }
            else
            {
                chaine = html.DropDownListFor(expressionSelected, list, new { htmlAttributes = new { @class = "form-control", @multiple = "multiple" } });
            }
            return MvcHtmlString.Create("<div class=\"form-group\">" +
                html.LabelFor(expression, htmlAttributes: new { @class = "control-label col-md-2" }) +
                colmd10 + chaine +
                "</div></div>");
        }
    }
}