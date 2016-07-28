namespace HitechCraft.WebApplication.Models
{
    using System.ComponentModel.DataAnnotations;
    using Properties;

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "EmailOrNickName", ResourceType = typeof(Resources))]
        public string EmailOrNickName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}