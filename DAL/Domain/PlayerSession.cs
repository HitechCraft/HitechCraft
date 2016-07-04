namespace DAL.Domain
{
    /// <summary>
    /// Player session model (for online-mode=true)
    /// </summary>
    public class PlayerSession : BaseEntity<PlayerSession>
    {
        #region Properties
        
        /// <summary>
        /// Minecraft client session
        /// </summary>
        public virtual string Session { get; set; }

        /// <summary>
        /// Minecraft client session
        /// </summary>
        public virtual string Server { get; set; }

        /// <summary>
        /// Unic client token
        /// </summary>
        public virtual string Token { get; set; }

        /// <summary>
        /// Player uuid
        /// </summary>
        public virtual string Md5 { get; set; }

        /// <summary>
        /// Player object
        /// </summary>
        public virtual Player Player { get; set; }

        #endregion
    }
}