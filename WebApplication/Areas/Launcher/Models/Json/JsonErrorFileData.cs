namespace WebApplication.Areas.Launcher.Models.Json
{
    public enum ErrorAction
    {
        Reupload,
        Remove
    }

    public class JsonErrorFileData
    {
        public string FileUrl { get; set; }

        public ErrorAction FileAction { get; set; }
    }
}