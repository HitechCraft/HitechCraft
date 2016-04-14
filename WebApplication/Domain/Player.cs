namespace WebApplication.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Player
    {
        [Key]
        public string Name { get; set; }

        /// <summary>
        /// Timed field
        /// </summary>
        public string PasswordMD5 { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}