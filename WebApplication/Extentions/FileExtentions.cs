namespace WebApplication.Extentions
{
    using System.IO;

    public static class FileExtentions
    {
        public static bool IsDirOrFileExists(string dir)
        {
            return File.Exists(dir);
        }
    }
}