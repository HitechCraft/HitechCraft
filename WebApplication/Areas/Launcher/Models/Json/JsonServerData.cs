namespace WebApplication.Areas.Launcher.Models.Json
{
    using System.Collections.Generic;

    public class JsonServerData : JsonServerStatusData
    {
        public int PlayerCount { get; set; }

        public int MaxPlayerCount { get; set; }
    }
}