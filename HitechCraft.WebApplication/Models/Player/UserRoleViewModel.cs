namespace HitechCraft.WebApplication.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserRoleViewModel
    {
        [Display(Name = "Имя пользователя")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Роль")]
        [Required]
        public string RoleId { get; set; }
    }
}