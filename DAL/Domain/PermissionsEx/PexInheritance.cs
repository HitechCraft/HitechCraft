namespace DAL.Domain.PermissionsEx
{
    /// <summary>
    /// PermissionsEx Inheritance
    /// </summary>
    public class PexInheritance
    {
        #region Properties

        /// <summary>
        /// Obj id
        /// </summary>
        public virtual int id { get; set; }

        /// <summary>
        /// Child entity
        /// </summary>
        public virtual string child { get; set; }

        /// <summary>
        /// Parent entity
        /// </summary>
        public virtual string parent { get; set; }

        /// <summary>
        /// Inheritance type
        /// </summary>
        public virtual int type { get; set; }

        /// <summary>
        /// World name
        /// </summary>
        public virtual string world { get; set; }

        #endregion
    }
}