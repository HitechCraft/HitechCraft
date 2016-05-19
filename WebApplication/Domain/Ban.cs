namespace WebApplication.Domain
{
    #region Using Directives
    
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion
    
    public class Ban
    {
        /// <summary>
        /// Player baned name
        /// </summary>
        [ForeignKey("Player")]
        public string name { get; set; }
        
        /// <summary>
        /// Baned reason
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// Admin or moderator, who banned player
        /// </summary>
        [ForeignKey("Admin")]
        public string admin { get; set; }
        
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

        public virtual Player Player { get; set; }

        public virtual Player Admin { get; set; }
    }
}