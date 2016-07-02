namespace WebApplication.Domain.PermissionsEx
{
    using System.ComponentModel.DataAnnotations;

    public class IKTransaction
    {
        /// <summary>
        /// Object ID
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Player
        /// </summary>
        public Player Player { get; set; }
    }
}