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
        [ForeignKey("Player")]
        public string name { get; set; }
        
        /// <summary>
        /// Last user login ip
        /// </summary>
        public string lastip { get; set; }

        public virtual Player Player { get; set; }
    }
}