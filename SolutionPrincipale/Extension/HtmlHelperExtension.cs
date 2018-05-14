using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace WebSport.Extension
{
    public static class HtmlHelperExtension
    {
        private static string colmd10 = "<div class=\"col-md-10\">";
        public static MvcHtmlString CustomEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return MvcHtmlString.Create("<div class=\"form-group\">" +
           html.LabelFor(expression, htmlAttributes: new { @class = "control-label col-md-2" }) +
           colmd10 +
                html.EditorFor(expression, new { htmlAttributes = new { @class = "form-control" } }) +
                html.ValidationMessageFor(expression, "", new { @class = "text-danger" }) +
            "</div></div>");
        }

        public static MvcHtmlString CustomDropdownListFor<TModel, TValueLabel, TValueSelected>
            (this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValueLabel>> expression,
            Expression<Func<TModel, TValueSelected>> expressionSelected,
            IEnumerable<SelectListItem> list)
        {
            return MvcHtmlString.Create("<div class=\"form-group\">" +
           html.LabelFor(expression, htmlAttributes: new { @class = "control-label col-md-2" }) +
           colmd10 +
            html.DropDownListFor(expressionSelected, list, new { htmlAttributes = new { @class = "form-control" } }) +
            "</div></div>");
        }

        public static MvcHtmlString CustomButtonSubmit<TModel>(this HtmlHelper<TModel> html, string label)
        {
            var firstDiv = new TagBuilder("div");
            firstDiv.AddCssClass("form-group");
            var secondDiv = new TagBuilder("div");
            secondDiv.AddCssClass("col-md-offset-2");
            secondDiv.AddCssClass("col-md-10");
            var input = new TagBuilder("input");
            input.MergeAttribute("type", "submit");
            input.MergeAttribute("value", label);
            input.AddCssClass("btn");
            input.AddCssClass("btn-default");

            secondDiv.InnerHtml = input.ToString(TagRenderMode.SelfClosing);
            firstDiv.InnerHtml = secondDiv.ToString();
            return MvcHtmlString.Create(firstDiv.ToString());

            //    return MvcHtmlString.Create("<div class=\"form-group\"><div class=\"col-md-offset-2 col-md-10\">" +
            //  "<input type = \"submit\" value=\"" + label + "\" class=\"btn btn-default\"/></div></div>");
        }
    }
}