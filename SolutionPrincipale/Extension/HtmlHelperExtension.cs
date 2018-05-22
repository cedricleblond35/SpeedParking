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
        /*public static MvcHtmlString CustomerDate<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string name)
        {
            return MvcHtmlString.Create("<input id=\"single_cal\" name=\"" +
                name +
                "\" class=\"form-control\" placeholder=\"MM / DD / YYYY HH:mm:ss\" type=\"text\" />");
        }*/

        /*public static MvcHtmlString CustomerDate<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string name)
        {
            return MvcHtmlString.Create("<div class='input-group date' id='datetimepicker1'><input type='text' class='form-control' name='"+name+"'/><span class='input-group-addon'><span class='glyphicon glyphicon-calendar'></span></span></div>");
        }*/
        /// <summary>
        /// Créer une icone (poubelle = supprimer ...)
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="html">@Html. devant</param>
        /// <param name="action">Action du controller à effectuer</param>
        /// <param name="glyphicon">Glyphicon bootstrap</param>
        /// <returns></returns>
        public static MvcHtmlString CustomIcone<TModel>(this HtmlHelper<TModel> html, UrlHelper url, int id, string action, string glyphicon)
        {
            return MvcHtmlString.Create("<a href=" + url.Action(action, new { id = id }) + "><span class=\"glyphicon glyphicon-" + glyphicon + "\" aria-hidden=\"true\"></span></a>");
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
        /// <summary>
        /// Select perso
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValueLabel"></typeparam>
        /// <typeparam name="TValueSelected"></typeparam>
        /// <param name="html">@Html. devant</param>
        /// <param name="expression">Liste à afficher</param>
        /// <param name="expressionSelected">Element de la liste selectionné à afficher</param>
        /// <param name="list">Personalisation de ce qui est affiché dans la liste (nom/prenom/...)</param>
        /// <param name="multiple"></param>
        /// <returns></returns>
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