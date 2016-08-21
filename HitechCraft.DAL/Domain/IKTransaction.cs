﻿namespace HitechCraft.DAL.Domain
{
    #region Using Directives

    using System;
    using Common.Entity;

    #endregion

    /// <summary>
    /// IKTransaction model
    /// </summary>
    public class IKTransaction : BaseEntity<IKTransaction>
    {
        #region Properties
        
        /// <summary>
        /// Transaction number
        /// </summary>
        public virtual string TransactionId { get; set; }

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