namespace HitechCraft.GameLauncherAPI.Managers
{
    using Core;

    public static class Config
    {
        /// <summary>
        /// Launcher master vversion (must equal with version in launcher)
        /// </summary>
        public static string MasterVersion => Const.LauncherMasterVersion;

        /// <summary>
        /// Key length of session and token keys
        /// </summary>
        public static int KeyLength => 32;

        /// <summary>
        /// Valid chars for key gens
        /// </summary>
        public static string KeyChars => Const.LauncherKeyChars;

        /// <summary>
        /// Player skin url
        /// </summary>
        public static string SkinsUrlString => Const.WebApiBaseUrl + "ClientServer/GetSkinImage?playerName=";

        /// <summary>
        /// Minecraft clients folder
        /// </summary>
        public static string ClientsDir => "Clients";

        /// <summary>
        /// Java folder
        /// </summary>
        public static string JavaDir => ClientsDir + "/" + "Java";
    }
}