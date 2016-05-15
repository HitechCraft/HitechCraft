using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class RoleViewModel
    {
        [Display(Name = "Имя роли")]
        [Required]
        public string Name { get; set; }
    }
}