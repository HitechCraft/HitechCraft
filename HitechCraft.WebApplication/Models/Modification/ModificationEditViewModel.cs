namespace HitechCraft.WebApplication.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ModificationEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Version { get; set; }

        public byte[] Image { get; set; }

        public string Description { get; set; }

        [Display(Name = "Видео на Youtube")]
        public string GuideVideoCode { get; set; }
    }
}