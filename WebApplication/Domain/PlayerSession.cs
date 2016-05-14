namespace WebApplication.Domain
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class PlayerSession
    {
        [ForeignKey("Player")]
        public string PlayerName { get; set; }

        public string Session { get; set; }

        public string Server { get; set; }

        public string Token { get; set; }

        public string Md5 { get; set; }

        public virtual Player Player { get; set; }
    }
}