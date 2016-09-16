using HitechCraft.Core.Entity.Base;

namespace HitechCraft.Core.Entity
{
    #region Using Directives

    

    #endregion

    /// <summary>
    /// Player shop item
    /// </summary>
    public class PlayerItem : BaseEntity<PlayerItem>
    {
        #region Properties
        
        /// <summary>
        /// Player object
        /// </summary>
        public virtual Player Player { get; set; }
        
        /// <summary>
        /// Shop item
        /// </summary>
        public virtual ShopItem Item { get; set; }
        
        /// <summary>
        /// Item count
        /// </summary>
        public virtual int Count { get; set; }

        #endregion
    }
}