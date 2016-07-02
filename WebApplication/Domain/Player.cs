namespace WebApplication.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public enum PlayerStatus
    {
        Player,
        Helper,
        Moderator,
        Administrator
    }

    public class Player
    {
        [Key]
        public string Name { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}