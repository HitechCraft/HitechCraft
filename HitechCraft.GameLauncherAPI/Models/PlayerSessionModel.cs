namespace HitechCraft.GameLauncherAPI.Models
{
    public class PlayerSessionModel
    {
        public int Id { get; set; }

        public string PlayerName { get; set; }

        public string Session { get; set; }

        public string Server { get; set; }

        public string Md5 { get; set; }

        public string Token { get; set; }
    }
}