namespace System.Web.Mvc
{
    using WebApplication.Areas.Launcher.Models.Json;

    public static class HtmlHelperExtentions
    {
        public static MvcHtmlString ServerStatusLabel(this HtmlHelper html, JsonServerStatus status, string text)
        {
            string textClass = "";

            switch (status)
            {
                case JsonServerStatus.Empty:
                    textClass = "label label-info";
                    break;
                case JsonServerStatus.Error:
                    textClass = "label label-danger";
                    break;
                case JsonServerStatus.Full:
                    textClass = "label label-warning";
                    break;
                case JsonServerStatus.Offline:
                    textClass = "label label-default";
                    break;
                case JsonServerStatus.Online:
                    textClass = "label label-success";
                    break;
            }

            return new MvcHtmlString("<span class='"+ textClass +"'>"+ text + "</span>");
        }
    }
}