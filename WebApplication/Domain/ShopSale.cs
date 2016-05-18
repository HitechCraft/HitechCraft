using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Domain
{
    public class ShopSale
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Item")]
        public string ItemId { get; set; }

        [Range(0.01,0.99)]
        public float Sale { get; set; }

        public ShopItem Item { get; set; }

    }
}