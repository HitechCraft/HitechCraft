using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Domain
{
    public class Currency
    {
        public int id { get; set; }

        [ForeignKey("Player")]
        public string username { get; set; }

        public double balance { get; set; }

        [DefaultValue(0)]
        public double realmoney { get; set; }

        public int status { get; set; }

        public virtual Player Player { get; set; }
    }
}