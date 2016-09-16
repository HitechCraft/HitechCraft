using HitechCraft.Core.Entity.Base;

namespace HitechCraft.Core.Entity
{
    #region Using Directives

    

    #endregion

    /// <summary>
    /// Sale of some item in Minecraft Shop
    /// </summary>
    public class ShopSale : BaseEntity<ShopSale>
    {
        #region Properties
        
        /// <summary>
        /// Sale value (0.01 - 1)
        /// </summary>
        public virtual float Amount { get; set; }

        /// <summary>
        /// Shop item
        /// </summary>
        public virtual ShopItem Item { get; set; }

        #endregion
    }
}