namespace WebApplication.Areas.Launcher.Models.Json
{
    public class JsonClientUserData
    {
        public string timestamp { get; set; }

        public string profileId { get; set; }

        public string profileName { get; set; }

        public JsonTextureData textures { get; set; }
    }
}