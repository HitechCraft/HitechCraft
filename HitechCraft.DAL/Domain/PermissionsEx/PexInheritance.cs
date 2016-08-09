namespace HitechCraft.DAL.Domain
{
    #region Using Directives

    using Common.Entity;
    using Common.Models.PermissionsEx;

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