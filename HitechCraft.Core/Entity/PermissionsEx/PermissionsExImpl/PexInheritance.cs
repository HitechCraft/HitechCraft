using HitechCraft.Core.Entity.Base;

namespace HitechCraft.Core.Entity
{
    #region Using Directives

    

    #endregion

    /// <summary>
    /// PermissionsEx Inheritance
    /// </summary>
    public class PexInheritance : BaseEntity<PexInheritance>, IPexInheritance
    {
        #region Properties
        
        /// <summary>
        /// Child entity
        /// </summary>
        public virtual string Child { get; set; }

        /// <summary>
        /// Parent entity
        /// </summary>
        public virtual string Parent { get; set; }

        /// <summary>
        /// Inheritance type
        /// </summary>
        public virtual int Type { get; set; }

        /// <summary>
        /// World name
        /// </summary>
        public virtual string World { get; set; }

        #endregion
    }
}