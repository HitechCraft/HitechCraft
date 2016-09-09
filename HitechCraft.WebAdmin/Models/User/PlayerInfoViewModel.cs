namespace HitechCraft.WebAdmin.Models
{
    using Common.Models.Enum;
    using System.ComponentModel.DataAnnotations;

    public class PlayerInfoViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Никнэйм")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Пол")]
        public Gender Gender { get; set; }

        [Required]
        [Display(Name = "Gonts")]
        public double Gonts { get; set; }

        [Required]
        [Display(Name = "Рубли")]
        public double Rubles { get; set; }
    }
}