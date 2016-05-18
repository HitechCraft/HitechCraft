namespace WebApplication.Areas.Launcher.Services
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    public static class LauncherManager
    {
        #region Methods

        public static List<string> GetRequiredFolderList(string clientName)
        {
            var clientDir = LauncherConfig.ClientsDir + "/" + clientName + "/";

            return new List<string>
            {
                LauncherConfig.ClientsDir + "/" + "assets.zip",
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