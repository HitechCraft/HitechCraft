namespace WebApplication.Domain.PermissionsEx
{
    /// <summary>
    /// PermissionsEx Inheritance
    /// </summary>
    public class PexInheritance
    {
        /// <summary>
        /// Obj id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Child entity
        /// </summary>
        public string child { get; set; }

        /// <summary>
        /// Parent entity
        /// </summary>
        public string parent { get; set; }

        /// <summary>
        /// Inheritance type
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// World name
        /// </summary>
        public string world { get; set; }
    }
}