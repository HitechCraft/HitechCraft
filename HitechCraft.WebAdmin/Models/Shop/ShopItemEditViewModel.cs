using System.ComponentModel.DataAnnotations;

namespace HitechCraft.WebAdmin.Models
{
    public class ShopItemEditViewModel
    {
        [Display(Name = "Игровой ID")]
        public string GameId { get; set; }
        
        [Required]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        
        public byte[] Image { get; set; }

        [Required]
        [Display(Name = "Цена (Gonts)")]
        public float Price { get; set; }

        [Display(Name = "Модификация")]
        public int ModificationId { get; set; }

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
    }
}