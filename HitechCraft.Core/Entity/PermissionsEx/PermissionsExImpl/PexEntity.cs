using HitechCraft.Core.Entity.Base;

namespace HitechCraft.Core.Entity
{
    #region Using Directives

    

    #endregion

    /// <summary>
    /// PermissionsEx Entity
    /// </summary>
    public class PexEntity : BaseEntity<PexEntity>, IPexEntity
    {
        #region Properties
        
        /// <summary>
        /// Entity name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Entity type
        /// </summary>
        public virtual int Type { get; set; }

        /// <summary>
        /// Is entity default
        /// </summary>
        public virtual int Default { get { return 0; } set { } }

        #endregion
    }
}