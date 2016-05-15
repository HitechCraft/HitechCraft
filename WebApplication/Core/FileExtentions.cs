namespace WebApplication.Managers
{
    using System.Web;
    using System.IO;
    using System;

    public static class FileManager
    {
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

        private static bool IsFileExists(string path)
        {
            return File.Exists(path);
        }

        private static bool IsDirectoryExists(string path)
        {
            return Directory.Exists(path);
        }
    }
}