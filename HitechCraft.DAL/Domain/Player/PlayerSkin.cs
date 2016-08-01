﻿namespace HitechCraft.DAL.Domain
{
    #region Using Directives

    using Common.Entity;

    #endregion

    /// <summary>
    /// Player texture (skin)
    /// </summary>
    public class PlayerSkin : BaseEntity<PlayerSkin>
    {
        #region Properties
        
        /// <summary>
        /// Player
        /// </summary>
        public virtual Player Player { get; set; }

        /// <summary>
        /// Byte array of file 
        /// </summary>
        public virtual byte[] Image { get; set; }

        #endregion
    }
}