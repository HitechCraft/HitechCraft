using System.Collections.Generic;
using HitechCraft.Core.Models.Json;

namespace HitechCraft.WebAdmin.Models
{
    public class ServerDetailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ClientVersion { get; set; }

        public string IpAddress { get; set; }

        public int Port { get; set; }

        public string Description { get; set; }
        
        public byte[] Image { get; set; }
        
        public int MapPort { get; set; }

        public JsonMinecraftServerData Data { get; set; }

        public IEnumerable<ServerModificationViewModel> Modifications { get; set; }
    }
}