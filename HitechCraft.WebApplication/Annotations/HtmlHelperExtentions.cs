using System.Web.Mvc.Html;

namespace System.Web.Mvc
{
    using HitechCraft.Common.Models.Json.MinecraftServer;

    public static class HtmlHelperExtentions
    {
        public static MvcHtmlString ServerStatusLabel(this HtmlHelper html, JsonMinecraftServerStatus status, string text)
        {
            string textClass = "";

            switch (status)
            {
                case JsonMinecraftServerStatus.Empty:
                    textClass = "label label-info";
                    break;
                case JsonMinecraftServerStatus.Error:
                    textClass = "label label-danger";
                    break;
                case JsonMinecraftServerStatus.Full:
                    textClass = "label label-warning";
                    break;
                case JsonMinecraftServerStatus.Offline:
                    textClass = "label label-default";
                    break;
                case JsonMinecraftServerStatus.Online:
                    textClass = "label label-success";
                    break;
            }

            return new MvcHtmlString("<span class='" + textClass + "'>" + text + "</span>");
        }

        public static MvcHtmlString ValidationSummaryStyled(this HtmlHelper html, bool excludePropertyErrors)
        {
            var summary = html.ValidationSummary(excludePropertyErrors);
            
            return summary != null && summary != new MvcHtmlString("") ? new MvcHtmlString("<div class='alert alert-danger'>" +
                                     "<h4>Обнаружены ошибки</h4>" +
                                     summary +
                                     "</div>") : new MvcHtmlString("");
        }
    }
}