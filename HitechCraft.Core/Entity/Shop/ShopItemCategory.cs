﻿using HitechCraft.Core.Entity.Base;

namespace HitechCraft.Core.Entity
{
    #region Using Directives

    

    #endregion

    /// <summary>
    /// Minecraft shop item category
    /// </summary>
    public class ShopItemCategory : BaseEntity<ShopItemCategory>
    {
        #region Properties
        
        /// <summary>
        /// Category name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Category description
        /// </summary>
        public virtual string Description { get; set; }

        #endregion
    }
}