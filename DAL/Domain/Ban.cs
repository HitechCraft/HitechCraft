namespace DAL.Domain
{
    #region Using Directives
    
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public enum BanType
    {
        #region Values

        //Бан
        Banned = 0,
        //Кик
        Kicked = 3,
        //Разбан
        Unbanned = 5,
        //В тюрьме
        InJail = 6,
        //Мут (запрет на чат)
        Muted = 7,
        //Выпущен из тюрьмы
        UnJail = 8,
        //Пермаментный бан
        Permabanned = 9

        #endregion
    }

    /// <summary>
    /// Ban action model
    /// </summary>
    public class Ban
    {
        #region Properties

        /// <summary>
        /// Object id
        /// </summary>
        public virtual int id { get; set; }

        /// <summary>
        /// Player banned
        /// </summary>
        public virtual Player Player { get; set; }

        /// <summary>
        /// Player banned name TODO: player name
        /// </summary>
        public virtual string name { get; set; }

        /// <summary>
        /// Baned reason
        /// </summary>
        public virtual string reason { get; set; }

        /// <summary>
        /// Admin, who banned player
        /// </summary>
        public virtual Player Admin { get; set; }

        /// <summary>
        /// Admin or moderator, who banned player TODO: player name
        /// </summary>
        public virtual string admin { get; set; }

        /// <summary>
        /// Time in unix
        /// </summary>
        public virtual int time { get; set; }

        /// <summary>
        /// Time for tempbans (unix)
        /// </summary>
        public virtual int temptime { get; set; }

        /// <summary>
        /// Type of ban
        /// </summary>
        public virtual BanType type { get; set; }

        #endregion
    }
}