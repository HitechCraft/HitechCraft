using System.Configuration;

namespace HitechCraft.Core
{
    public static class Const
    {
        public static string Domain => ConfigurationManager.AppSettings["Domain"];

        public static string BaseUrl => ConfigurationManager.AppSettings["BaseUrl"];

        public static string TopCraftKey => ConfigurationManager.AppSettings["TopCraftKey"];

        public static string McTopKey => ConfigurationManager.AppSettings["MCTopKey"];

        public static string McTopSuKey => ConfigurationManager.AppSettings["MCTopSuKey"];

        public static string LauncherMasterVersion => ConfigurationManager.AppSettings["LauncherMasterVersion"];

        public static string LauncherKeyChars => ConfigurationManager.AppSettings["LauncherKeyChars"];
    }
}