namespace WebApplication.Areas.Launcher.Models.Json
{
    using System.Collections.Generic;

    public class JsonServerData : JsonServerStatusData
    {
        public string ServerName { get; set; }

        public string ServerDescription { get; set; }

        public string ClientVersion { get; set; }

        public string IpAddress { get; set; }

        public int Port { get; set; }

        public int PlayerCount { get; set; }

        public int MaxPlayerCount { get; set; }

        public IEnumerable<string> ServerModifications { get; set; }
    }
}