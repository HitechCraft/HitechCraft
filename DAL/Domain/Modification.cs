namespace DAL.Domain
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion
    
    /// <summary>
    /// Minecraft modification model
    /// </summary>
    public class Modification
    {
        #region Properties

        /// <summary>
        /// Object id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Mode name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Mode version
        /// </summary>
        public virtual string Version { get; set; }

        /// <summary>
        /// Mode description
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// ServerModification collection
        /// </summary>
        public virtual ISet<ServerModification> ServerModifications { get; set; }

        #endregion
    }
}