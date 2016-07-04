namespace DAL.Domain
{
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
        /// Player name TODO: посмотреть можно ли в маппингах соответствия настраивать. Типо чтобы имя поля не PlayerName было а nickname
        /// </summary>
        public virtual string nickname { get; set; }

        /// <summary>
        /// Shop item
        /// </summary>
        public virtual ShopItem Item { get; set; }

        /// <summary>
        /// Shop item
        /// </summary>
        public virtual string item_id { get; set; }

        /// <summary>
        /// Item amount (count...)
        /// </summary>
        public virtual int item_amount { get; set; }

        #endregion
    }
}