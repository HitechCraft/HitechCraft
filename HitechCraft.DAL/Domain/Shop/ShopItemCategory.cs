namespace HitechCraft.DAL.Domain
{
    #region Using Directives

    using Common.Entity;

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