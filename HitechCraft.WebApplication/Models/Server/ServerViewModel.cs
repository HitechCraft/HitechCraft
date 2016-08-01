namespace HitechCraft.WebApplication.Models
{
    using System.Collections.Generic;
    using Common.Models.Json.MinecraftServer;

    public class ServerViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Address { get; set; }

        public string Description { get; set; }

        public string ClientVersion { get; set; }

        public byte[] Image { get; set; }
        
        public JsonMinecraftServerData Data { get; set; }
    }
}