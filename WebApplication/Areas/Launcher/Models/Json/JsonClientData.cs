namespace WebApplication.Areas.Launcher.Models.Json
{
    using System.Collections.Generic;

    public class JsonClientData
    {
        public ICollection<JsonClientFilesData> FilesData { get; set; }

        public string ClientName { get; set; }
    }
}
