namespace WebApplication.Areas.Launcher.Models.Json
{
    using System.Collections.Generic;

    public class JsonClientFilesStatusData : JsonStatusData
    {
        public ICollection<JsonErrorFileData> FileData { get; set; }
    }
}