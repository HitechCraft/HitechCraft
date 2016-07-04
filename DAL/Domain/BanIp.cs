namespace DAL.Domain
{
    /// <summary>
    /// BanIP action model
    /// </summary>
    public class BanIp : BaseEntity<BanIp>
    {
        #region Properties
        
        /// <summary>
        /// Player object
        /// </summary>
        public virtual Player Player { get; set; }

        /// <summary>
        /// Player Name TODO: player name!!!
        /// </summary>
        public virtual string name { get; set; }

        /// <summary>
        /// Last user login ip
        /// </summary>
        public virtual string lastip { get; set; }

        #endregion
    }
}