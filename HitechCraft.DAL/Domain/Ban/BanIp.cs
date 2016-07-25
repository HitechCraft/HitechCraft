namespace HitechCraft.DAL.Domain
{
    #region Using Directives

    using Common.Entity;

    #endregion

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
        /// Last user login ip
        /// </summary>
        public virtual string LastIp { get; set; }

        #endregion
    }
}