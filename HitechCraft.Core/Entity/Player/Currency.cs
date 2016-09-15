namespace HitechCraft.Core.Entity
{
    #region Using Directives

    

    #endregion

    /// <summary>
    /// Player currency model
    /// </summary>
    public class Currency : BaseEntity<Currency>
    {
        #region Properties
        
        /// <summary>
        /// Player object
        /// </summary>
        public virtual Player Player { get; set; }
        
        /// <summary>
        /// Player Gonts
        /// </summary>
        public virtual double Gonts { get; set; }

        /// <summary>
        /// Player rubs
        /// </summary>
        public virtual double Rubels { get; set; }

        /// <summary>
        /// Status (?)
        /// </summary>
        public virtual int Status { get; set; }

        #endregion
    }
}