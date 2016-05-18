namespace WebApplication.Managers
{
    #region Using Directives

    using System.Web;
    using System.IO;
    using System;

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
            var sitePath = HttpContext.Current.Server.MapPath(path);

            try
            {
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

        #region Private Methods

        private static bool IsFileExists(string path)
        {
            return File.Exists(path);
        }

        private static bool IsDirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        #endregion
    }
}