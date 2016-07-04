﻿namespace DAL.Domain.PermissionsEx
{
    /// <summary>
    /// PermissionsEx Entity
    /// </summary>
    public class PexEntity : BaseEntity<PexEntity>
    {
        #region Properties
        
        /// <summary>
        /// Entity name
        /// </summary>
        public virtual string name { get; set; }

        /// <summary>
        /// Entity type
        /// </summary>
        public virtual int type { get; set; }

        /// <summary>
        /// Is entity default
        /// </summary>
        public virtual int isDefault { get { return 0; } set { } }

        #endregion
    }
}