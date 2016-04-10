using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Domain
{
    public class Ban
    {
        /// <summary>
        /// Player baned name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Baned User
        /// </summary>
        [ForeignKey("BanedUser")]
        public string BanedUserId { get; set; }

        /// <summary>
        /// Baned reason
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// Admin or moderator, who banned player
        /// </summary>
        public string admin { get; set; }

        /// <summary>
        /// Admin User
        /// </summary>
        [ForeignKey("AdminUser")]
        public string AdminUserId { get; set; }

        /// <summary>
        /// Time in unix
        /// </summary>
        public int time { get; set; }

        /// <summary>
        /// Time for tempbans (unix)
        /// </summary>
        public int temptime { get; set; }

        /// <summary>
        /// Object id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Type of ban
        /// 0 - banned
        /// 3 - kick
        /// 5 - unbanned
        /// 6 - jail
        /// 7 - mute
        /// 8 - unjail
        /// 9 - permaban
        /// </summary>
        public int type { get; set; }

        public virtual ApplicationUser BanedUser { get; set; }

        public virtual ApplicationUser AdminUser { get; set; }
    }
}