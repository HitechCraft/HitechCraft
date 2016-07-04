namespace DAL.Domain.PermissionsEx
{
    /// <summary>
    /// PermissionsEx Inheritance
    /// </summary>
    public class PexInheritance : BaseEntity<PexInheritance>
    {
        #region Properties
        
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