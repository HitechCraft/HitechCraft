namespace WebApplication.Models
{
    using Areas.Launcher.Models.Json;
    using System.Collections.Generic;

    public class ServerViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ClientVersion { get; set; }

        public string IpAddress { get; set; }

        public int Port { get; set; }

        public string Description { get; set; }
        
        public byte[] Image { get; set; }
        
        public int MapPort { get; set; }

        public IEnumerable<ServerModificationViewModel> Modifications { get; set; }

        public JsonServerData ServerData { get; set; }
    }
}