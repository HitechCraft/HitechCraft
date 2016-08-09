namespace HitechCraft.DAL.Domain
{
    #region Using Directives

    using System.Collections.Generic;
    using Common.Entity;

    #endregion

    /// <summary>
    /// Minecraft modification model
    /// </summary>
    public class Modification : BaseEntity<Modification>
    {
        #region Properties
        
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
        /// Mod image
        /// </summary>
        public virtual byte[] Image { get; set; }

        /// <summary>
        /// Youtube video code  
        /// </summary>
        public virtual string GuideVideo { get; set; }

        /// <summary>
        /// ServerModification collection
        /// </summary>
        public virtual ISet<ServerModification> ServerModifications { get; set; }

        #endregion
    }
}