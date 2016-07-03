namespace DAL.Domain
{
    /// <summary>
    /// Player currency model
    /// </summary>
    public class Currency
    {
        #region Properties

        /// <summary>
        /// Object id
        /// </summary>
        public virtual int id { get; set; }

        /// <summary>
        /// Player object
        /// </summary>
        public virtual Player Player { get; set; }

        /// <summary>
        /// Username TODO: player name
        /// </summary>
        public virtual string username { get; set; }

        /// <summary>
        /// Player Gonts
        /// </summary>
        public virtual double balance { get; set; }

        /// <summary>
        /// Player rubs
        /// </summary>
        public virtual double realmoney { get; set; }

        /// <summary>
        /// Status (?)
        /// </summary>
        public virtual int status { get; set; }

        #endregion
    }
}