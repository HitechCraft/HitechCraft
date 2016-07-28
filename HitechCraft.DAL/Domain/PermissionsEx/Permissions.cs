namespace HitechCraft.DAL.Domain
{
    #region Using Directives

    using Common.Entity;
    using Common.Models.PermissionsEx;

    #endregion

    /// <summary>
    /// PermissionsEx
    /// </summary>
    public class Permissions : BaseEntity<Permissions>, IPermissions
    {
        #region Properties
        
        /// <summary>
        /// Obj name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Permission type
        /// </summary>
        public virtual int Type { get; set; }

        /// <summary>
        /// Permission string
        /// </summary>
        public virtual string Permission { get; set; }

        /// <summary>
        /// World name
        /// </summary>
        public virtual string World { get; set; }

        /// <summary>
        /// Permission value
        /// </summary>
        public virtual string Value { get; set; }

        #endregion
    }
}