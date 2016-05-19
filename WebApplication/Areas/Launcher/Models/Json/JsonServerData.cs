namespace WebApplication.Areas.Launcher.Models.Json
{
    public class JsonServerData
    {
        public string Hostname { get; set; }

        public int PlayerCount { get; set; }

        public int MaxPlayerCount { get; set; }

        public int Protocol { get; set; }

        public string Version { get; set; }
    }
}