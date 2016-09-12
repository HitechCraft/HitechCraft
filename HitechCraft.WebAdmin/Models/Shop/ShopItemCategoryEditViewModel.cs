using System.ComponentModel.DataAnnotations;

namespace HitechCraft.WebAdmin.Models
{
    public class ShopItemCategoryEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}