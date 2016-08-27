namespace HitechCraft.WebApplication.Models
{
    using System.ComponentModel.DataAnnotations;
    using Properties;

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(Resources))]
        public string Email { get; set; }
    }

}