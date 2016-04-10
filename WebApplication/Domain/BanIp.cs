using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Domain
{
    public class BanIp
    {
        /// <summary>
        /// Object Id   
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        [ForeignKey("User")]
        public string UserId { get; set; }

        /// <summary>
        /// Last user login ip
        /// </summary>
        public string lastip { get; set; }

        public ApplicationUser User { get; set; }
    }
}