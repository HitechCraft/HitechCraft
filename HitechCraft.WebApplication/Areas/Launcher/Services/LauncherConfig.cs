namespace HitechCraft.WebApplication.Areas.Launcher.Services
{
    using System.Web.Configuration;

    public static class LauncherConfig
    {
        /// <summary>
        /// Launcher master vversion (must equal with version in launcher)
        /// </summary>
        public static string MasterVersion
        {
            get { return "v1.0beta"; }
        }

        /// <summary>
        /// Key length of session and token keys
        /// </summary>
        public static int KeyLength
        {
            get { return 32; }
        }

        /// <summary>
        /// Valid chars for key gens
        /// </summary>
        public static string KeyChars
        {
            get { return "abcdefghijklmnopqrstuvwxyz1234567890"; }
        }

        /// <summary>
        /// Launcher site folder
        /// </summary>
        public static string DataDir
        {
            //TODO сделать нормальный редирект на Launcher (проверять через web request), без Areas
            get { return "/" + "Areas" + "/" + "Launcher"; }
        }

        /// <summary>
        /// Player skin url
        /// </summary>
        public static string SkinsUrlString
        {
            get { return WebConfigurationManager.AppSettings["BaseUrl"] + "Account/GetSkinImage?userName="; }
        }

        /// <summary>
        /// Minecraft clients folder
        /// </summary>
        public static string ClientsDir
        {
            get { return DataDir + "/" + "Clients"; }
        }
    }
}