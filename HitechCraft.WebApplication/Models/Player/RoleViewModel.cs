namespace HitechCraft.WebApplication.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RoleViewModel
    {
        [Display(Name = "Имя роли")]
        [Required]
        public string Name { get; set; }
    }
}