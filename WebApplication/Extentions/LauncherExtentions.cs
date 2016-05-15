using System.Collections.Generic;
using System.IO;

namespace WebApplication.Extentions
{
    public static class LauncherExtentions
    {
        #region Properties

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
            get { return 64; }
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
        public static string LauncherDataDir
        {
            get { return "~/" + "Launcher"; }
        }

        /// <summary>
        /// Minecraft clients folder
        /// </summary>
        public static string LauncherClientsDir
        {
            get { return LauncherDataDir + "/" + "Clients"; }
        }

        #endregion

        #region Methods
        
        public static List<string> GetRequiredFolderList(string clientName)
        {
            var clientDir = LauncherClientsDir + "/" + clientName + "/";

            return new List<string>
            {
                LauncherClientsDir + "/" + "assets.zip",
                clientDir + "bin" + "/",
                clientDir + "natives" + "/",
                clientDir + "mods" + "/",
                clientDir + "coremods" + "/",
                clientDir + "config.zip"
            };
        }

        #endregion
    }
}