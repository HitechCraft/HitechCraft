namespace WebApplication.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    [Bind(Include = "Id, GameId, Name, Description, Amount")]
    public class ShopItem
    {
        [Key]
        public string GameId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        //todo сделать возможность установки цен по разным видам валюты
        public float Price { get; set; }
    }
}