using HitechCraft.Common.Models.Json.MinecraftServer;

namespace HitechCraft.WebAdmin.Models
{
    public class ServerViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string IpAddress { get; set; }

        public string ServerPort { get; set; }

        public string MapPort { get; set; }

        public string Description { get; set; }

        public string ClientVersion { get; set; }

        public byte[] Image { get; set; }
        
        public JsonMinecraftServerData Data { get; set; }
    }
}