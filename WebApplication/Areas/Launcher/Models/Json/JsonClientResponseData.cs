namespace WebApplication.Areas.Launcher.Models.Json
{
    public class JsonClientResponseData
    {
        public string id { get; set; }

        public string name { get; set; }

        public JsonClientPropertiesData[] properties { get; set; }
    }
}