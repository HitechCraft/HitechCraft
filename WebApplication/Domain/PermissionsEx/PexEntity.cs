namespace WebApplication.Domain.PermissionsEx
{
    /// <summary>
    /// PermissionsEx Entity
    /// </summary>
    public class PexEntity
    {
        /// <summary>
        /// Obj id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Entity name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Entity type
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// Is entity default
        /// </summary>
        public int isDefault { get { return 0; } set { } }
    }
}