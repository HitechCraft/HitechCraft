namespace WebApplication.Domain
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    [Bind(Include = "id, nickname, item_id, item_amount, User, Item")]
    public class PlayerItem
    {
        public int id { get; set; }
        
        public string nickname { get; set; }

        [ForeignKey("User")]
        public string user_id { get; set; }

        [ForeignKey("Item")]
        public int item_id { get; set; }

        public int item_amount { get; set; }
        
        public virtual ShopItem Item { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}