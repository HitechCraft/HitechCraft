namespace DAL.Domain.PermissionsEx
{
    /// <summary>
    /// PermissionsEx
    /// </summary>
    public class Permission
    {
        #region Properties

        /// <summary>
        /// Obj id
        /// </summary>
        public virtual int id { get; set; }

        /// <summary>
        /// Obj name
        /// </summary>
        public virtual string name { get; set; }

        /// <summary>
        /// Permission type
        /// </summary>
        public virtual int type { get; set; }

        /// <summary>
        /// Permission string
        /// </summary>
        public virtual string permission { get; set; }

        /// <summary>
        /// World name
        /// </summary>
        public virtual string world { get; set; }

        /// <summary>
        /// Permission value
        /// </summary>
        public virtual string value { get; set; }

        #endregion
    }
}