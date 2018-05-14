using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApplication1.Helpers
{
    public static class ImageHelper
    {

        public static string Image(this HtmlHelper helper, string id, string url, string alternateText, object htmlAttributes)
        {
            // Crée le TagBuilder
            var builder = new TagBuilder("img");

            // Crée un id valide
            builder.GenerateId(id);

            // Ajoute les attributs au TagBuilder
            builder.MergeAttribute("src", url);
            builder.MergeAttribute("alt", alternateText);
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            // Fait un rendu du tag
            return builder.ToString(TagRenderMode.SelfClosing);
        }

    }
}