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
    }
}