namespace HitechCraft.GameLauncherAPI.Managers
{
    using System.Web.Configuration;

    public static class Config
    {
        /// <summary>
        /// Launcher master vversion (must equal with version in launcher)
        /// </summary>
        public static string MasterVersion
        {
            get { return WebConfigurationManager.AppSettings["LauncherMasterVersion"]; }
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
        /// Player skin url
        /// </summary>
        public static string SkinsUrlString
        {
            get { return WebConfigurationManager.AppSettings["BaseUrl"] + "ClientServer/GetSkinImage?playerName="; }
        }

        /// <summary>
        /// Minecraft clients folder
        /// </summary>
        public static string ClientsDir
        {
            get { return "Clients"; }
        }
    }
}