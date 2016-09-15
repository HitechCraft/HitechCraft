using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using HitechCraft.Core.Models.Enum;

namespace HitechCraft.GameLauncherAPI.Managers
{
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

        /// <summary>
        /// Returns client required folders and files
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public static List<string> GetRequiredFolderList(string clientName)
        {
            var clientDir = Config.ClientsDir + "/" + clientName + "/";

            return new List<string>
            {
                Config.ClientsDir + "/" + "assets.zip",
                clientDir + "bin" + "/",
                clientDir + "natives" + "/",
                clientDir + "mods" + "/",
                clientDir + "coremods" + "/",
                clientDir + "config.zip"
            };
        }

        /// <summary>
        /// Returns Java path by system bit
        /// </summary>
        /// <param name="systemBit"></param>
        /// <returns></returns>
        public static string GetJavaPath(SystemBit systemBit)
        {
            string javaFilesPath = FileManager.GetServerPath(Config.JavaDir).Replace("\\", "/");

            return javaFilesPath;
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

        public static string GetServerPath(string path)
        {
            var serverPath = HttpContext.Current.Server.MapPath(path);

            return serverPath;
        }

        private static string GetClientPath(string serverPath, string clientName)
        {
            //TODO переписать!!!
            var clientPath = (HttpRuntime.AppDomainAppPath + "Launcher\\" + Config.ClientsDir.Replace("/", "\\")) + "\\" + clientName;

            var path = serverPath.Replace(clientPath, "").Replace("\\\\", "\\");

            return path;
        }

        #endregion
    }
}
