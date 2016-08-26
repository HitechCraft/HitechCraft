namespace HitechCraft.DAL.Domain
{
    #region Using Directives

    using Common.Entity;

    #endregion

    /// <summary>
    /// Relation model of Server and Modification
    /// </summary>
    public class ServerModification : BaseEntity<ServerModification>
    {
        #region Properties
        
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