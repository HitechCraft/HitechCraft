namespace DAL.Domain
{
    /// <summary>
    /// Sale of some item in Minecraft Shop
    /// </summary>
    public class ShopSale
    {
        #region Properties

        /// <summary>
        /// Object id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Sale value (0.01 - 1)
        /// </summary>
        public virtual float Sale { get; set; }

        /// <summary>
        /// Shop item
        /// </summary>
        public virtual ShopItem Item { get; set; }

        #endregion
    }
}