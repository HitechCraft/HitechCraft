namespace DAL.Domain
{
    /// <summary>
    /// Relation model of Server and Modification
    /// </summary>
    public class ServerModification
    {
        #region Properties

        /// <summary>
        /// Object id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Server object
        /// </summary>
        public virtual Server Server { get; set; }

        /// <summary>
        /// Modification object
        /// </summary>
        public virtual Modification Modification { get; set; }

        #endregion
    }
}