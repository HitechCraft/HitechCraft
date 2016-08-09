namespace HitechCraft.WebApplication.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
    }
}