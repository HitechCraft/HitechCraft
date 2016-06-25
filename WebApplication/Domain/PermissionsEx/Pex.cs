namespace WebApplication.Domain.PermissionsEx
{
    /// <summary>
    /// PermissionsEx
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// Obj id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Obj name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Permission type
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// Permission string
        /// </summary>
        public string permission { get; set; }

        /// <summary>
        /// World name
        /// </summary>
        public string world { get; set; }

        /// <summary>
        /// Permission value
        /// </summary>
        public string value { get; set; }
    }
}