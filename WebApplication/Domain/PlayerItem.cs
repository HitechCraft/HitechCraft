namespace WebApplication.Domain
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    [Bind(Include = "id, nickname, item_id, item_amount, User, Item")]
    public class PlayerItem
    {
        public int id { get; set; }

        [ForeignKey("Player")]
        public string nickname { get; set; }

        [ForeignKey("Item")]
        public string item_id { get; set; }

        public int item_amount { get; set; }
        
        public virtual ShopItem Item { get; set; }

        public virtual Player Player { get; set; }
    }
}