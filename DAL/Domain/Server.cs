namespace DAL.Domain
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion
    
    /// <summary>
    /// Server model
    /// </summary>
    public class Server : BaseEntity<Server>
    {
        #region Properties
        
        /// <summary>
        /// Server name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Server description
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Byte array of server image
        /// </summary>
        public virtual byte[] Image { get; set; }

        /// <summary>
        /// Client version for cur server
        /// </summary>
        public virtual string ClientVersion { get; set; }

        /// <summary>
        /// Server IP adress
        /// </summary>
        public virtual string IpAddress { get; set; }

        /// <summary>
        /// Server port
        /// </summary>
        public virtual int Port { get; set; }

        /// <summary>
        /// Port for Dynmap
        /// </summary>
        public virtual int MapPort { get; set; }

        /// <summary>
        /// ServerModifications collection
        /// </summary>
        public virtual ISet<ServerModification> ServerModifications { get; set; }

        #endregion
    }
}