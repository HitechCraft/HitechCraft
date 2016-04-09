namespace WebApplication.Domain
{
    using System.Web.Mvc;

    [Bind(Include = "Id, Name, Description, Amount")]
    public class ShopItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        //todo сделать возможность установки цен по разным видам валюты
        public float Amount { get; set; }
    }
}