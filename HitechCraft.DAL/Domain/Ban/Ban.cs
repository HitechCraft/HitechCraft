namespace HitechCraft.DAL.Domain
{
    #region Using Directives

    using Common.Entity;
    using Common.Models.Enum;

    #endregion

    /// <summary>
    /// Ban action model
    /// </summary>
    public class Ban : BaseEntity<Ban>
    {
        #region Properties

        /// <summary>
        /// Player banned
        /// </summary>
        public virtual Player Player { get; set; }
        
        /// <summary>
        /// Baned reason
        /// </summary>
        public virtual string Reason { get; set; }

        /// <summary>
        /// Admin, who banned player
        /// </summary>
        public virtual Player Admin { get; set; }
        
        /// <summary>
        /// Time in unix
        /// </summary>
        public virtual int Time { get; set; }

        /// <summary>
        /// Time for tempbans (unix)
        /// </summary>
        public virtual int TempTime { get; set; }

        /// <summary>
        /// Type of ban
        /// </summary>
        public virtual BanType Type { get; set; }

        #endregion
    }
}