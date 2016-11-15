using System;
using System.Globalization;

namespace HitechCraft.WebApplication.Manager
{
    using System.Web.Configuration;

    public static class Const
    {
        public static string WebAppDomain => WebConfigurationManager.AppSettings["WebAppDomain"];
        public static string WebAppBaseUrl => "http://" + WebConfigurationManager.AppSettings["WebAppDomain"] + "/";

        public static string WebApiDomain => WebConfigurationManager.AppSettings["WebApiDomain"];
        public static string WebApiBaseUrl => "http://" + WebConfigurationManager.AppSettings["WebApiDomain"] + "/";

        public static string WebAdminDomain => WebConfigurationManager.AppSettings["WebAdminDomain"];
        public static string WebAdminBaseUrl => "http://" + WebConfigurationManager.AppSettings["WebAdminDomain"] + "/";

        public static bool ShopEnable => Convert.ToBoolean(WebConfigurationManager.AppSettings["ShopEnable"]);

        public static float GontToRubValue => float.Parse(WebConfigurationManager.AppSettings["GontToRubValue"], CultureInfo.InvariantCulture);
        public static float RubToGontValue => float.Parse(WebConfigurationManager.AppSettings["RubToGontValue"], CultureInfo.InvariantCulture);

        public static string TopCraftKey => WebConfigurationManager.AppSettings["TopCraftKey"];

        public static string McTopKey => WebConfigurationManager.AppSettings["MCTopKey"];

        public static string McTopSuKey => WebConfigurationManager.AppSettings["MCTopSuKey"];
    }
}