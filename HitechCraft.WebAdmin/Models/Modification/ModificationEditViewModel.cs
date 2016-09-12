using System.ComponentModel.DataAnnotations;

namespace HitechCraft.WebAdmin.Models
{
    public class ModificationEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Версия")]
        public string Version { get; set; }

        public byte[] Image { get; set; }

        [Display(Name = "Видео на Youtube")]
        public string GuideVideoCode { get; set; }
    }
}