namespace DAL.Domain
{
    /// <summary>
    /// Minecraft shop item category
    /// </summary>
    public class ShopItemCategory
    {
        #region Properties

        /// <summary>
        /// Object id
        /// </summary>
        public virtual int Id { get; set; }

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