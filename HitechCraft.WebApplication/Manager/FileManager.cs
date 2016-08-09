namespace HitechCraft.WebApplication.Manager
{
    #region Using Directives

    using System;
    using System.IO;
    using System.Web;
    using Areas.Launcher.Services;

    #endregion

    public static class FileManager
    {
        /// <summary>
        /// Check is valid file or dir path
        /// </summary>
        /// <param name="path">File or dir path</param>
        /// <returns></returns>
        public static bool IsDirOrFileExists(string path)
        {
            try
            {
                var sitePath = GetServerPath(path);

                var attrs = File.GetAttributes(sitePath);

                if (attrs.HasFlag(FileAttributes.Directory))
                    return IsDirectoryExists(sitePath);

                return IsFileExists(sitePath);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Check is path as directory
        /// </summary>
        /// <param name="path">File or dir path</param>
        /// <returns></returns>
        public static bool IsDirectory(string path)
        {
            var attrs = File.GetAttributes(GetServerPath(path));

            return attrs.HasFlag(FileAttributes.Directory);
        }

        /// <summary>
        /// Check is path as file
        /// </summary>
        /// <param name="path">File or dir path</param>
        /// <returns></returns>
        public static bool IsFile(string path)
        {
            return !IsDirectory(path);
        }

        /// <summary>
        /// Get file array from http
        /// </summary>
        /// <param name="path">Site path</param>
        /// <param name="searchPattern">File mask</param>
        /// <param name="searchOption">Search option</param>
        /// <returns></returns>
        public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.GetFiles(GetServerPath(path), searchPattern, searchOption);
        }

        /// <summary>
        /// Returns site client path
        /// </summary>
        /// <param name="serverPath">Server path</param>
        /// <param name="clientName">Client name</param>
        /// <returns></returns>
        public static string GetClientFilePath(string serverPath, string clientName)
        {
            return GetClientPath(serverPath, clientName).Replace("\\", "/");
        }

        /// <summary>
        /// Returns byte[] of downloaded file
        /// </summary>
        /// <param name="absoluteUrl">File url</param>
        /// <returns></returns>
        public static byte[] DownloadFile(string absoluteUrl)
        {
            using (var client = new System.Net.WebClient())
            {
                return client.DownloadData(absoluteUrl);
            }
        }

        #region Private Methods

        private static bool IsFileExists(string path)
        {
            return File.Exists(path);
        }

        private static bool IsDirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        private static string GetServerPath(string path)
        {
            var serverPath = HttpContext.Current.Server.MapPath(path);

            return serverPath;
        }

        private static string GetAbsolutePath(string serverPath)
        {
            return serverPath.Replace(HttpRuntime.AppDomainAppPath, "");
        }

        private static string GetClientPath(string serverPath, string clientName)
        {
            //TODO переписать!!!
            var clientPath = (HttpRuntime.AppDomainAppPath + LauncherConfig.ClientsDir.Replace("/", "\\")) + "\\" + clientName;

            var path = serverPath.Replace(clientPath.Replace("\\\\", "\\"), "");

            return path;
        }

        #endregion
    }
}