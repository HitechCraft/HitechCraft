namespace HitechCraft.DAL.Domain
{
    #region Using Directives

    using Common.Entity;

    #endregion

    /// <summary>
    /// Minecraft shop item
    /// </summary>
    public class ShopItem : BaseEntity<ShopItem>
    {
        #region Properties

        /// <summary>
        /// Ingame item id
        /// </summary>
        public virtual string GameId { get; set; }

        /// <summary>
        /// Item name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Item description
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Byte array of item icon
        /// </summary>
        public virtual byte[] Image { get; set; }

        /// <summary>
        /// Item price
        /// </summary>
        //TODO: сделать возможность установки цен по разным видам валюты
        public virtual float Price { get; set; }

        /// <summary>
        /// Item category
        /// </summary>
        public virtual ShopItemCategory ItemCategory { get; set; }

        #endregion
    }
}