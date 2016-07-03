namespace DAL.Domain
{
    #region Using Directives
    
    using System;

    #endregion

    /// <summary>
    /// IKTransaction model
    /// </summary>
    public class IKTransaction
    {
        #region Properties

        /// <summary>
        /// Object ID
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// Player
        /// </summary>
        public virtual Player Player { get; set; }

        /// <summary>
        /// Time of creating transaction
        /// </summary>
        public virtual DateTime Time { get; set; }

        #endregion
    }
}