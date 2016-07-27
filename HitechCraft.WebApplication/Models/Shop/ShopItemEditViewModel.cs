namespace HitechCraft.WebApplication.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ShopItemEditViewModel
    {
        public string GameId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        
        public byte[] Image { get; set; }

        [Required]
        public int Price { get; set; }
        
        public int ModificationId { get; set; }

        public int CategoryId { get; set; }
    }
}