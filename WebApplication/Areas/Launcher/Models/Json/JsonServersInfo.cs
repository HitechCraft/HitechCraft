namespace WebApplication.Areas.Launcher.Models.Json
{
    using System.Collections.Generic;

    public class JsonServersInfo
    {
        public ICollection<JsonServerData> ServerData { get; set; }

        //TODO: убрать?
        public int ServerCount { get; set; }
    }
}