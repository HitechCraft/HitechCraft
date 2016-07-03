namespace DAL.Domain
{
    /// <summary>
    /// Player texture (skin)
    /// </summary>
    public class PlayerSkin
    {
        #region Properties

        /// <summary>
        /// Object id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Byte array of file 
        /// </summary>
        public virtual byte[] Image { get; set; }

        /// <summary>
        /// Player
        /// </summary>
        public virtual Player Player { get; set; }

        #endregion
    }
}