using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Domain
{
    public class Currency
    {
        public int id { get; set; }

        public string username { get; set; }

        [ForeignKey("User")]
        public string user_id { get; set; }

        public double balance { get; set; }

        [DefaultValue(0)]
        public double realmoney { get; set; }

        public int status { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}