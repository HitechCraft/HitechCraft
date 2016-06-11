namespace WebApplication.Areas.Launcher.Models.Json
{
    public enum FileAction
    {
        Load,
        Remove
    }

    public class JsonErrorFileData
    {
        public string FilePath { get; set; }

        public FileAction FileAction { get; set; }
    }
}