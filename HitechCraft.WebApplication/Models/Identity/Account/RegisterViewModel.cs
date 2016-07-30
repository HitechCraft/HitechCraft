namespace HitechCraft.WebApplication.Models
{
    using System.ComponentModel.DataAnnotations;
    using Properties;
    using Common.Models.Enum;

    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Resources))]
        [MinLength(4, ErrorMessageResourceName = "ErrorMinLength", ErrorMessageResourceType = typeof(Resources))]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessageResourceName = "ErrorUserNameChars", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "NickName", ResourceType = typeof(Resources))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Resources))]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Resources))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Gender", ResourceType = typeof(Resources))]
        public Gender Gender { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Resources))]
        [StringLength(100, ErrorMessageResourceName = "ErrorStringMinLength", MinimumLength = 6, ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Password)]
        [Display(Name = "PasswordConfirm", ResourceType = typeof(Resources))]
        [Compare("Password", ErrorMessageResourceName = "ErrorPasswordsCompare", ErrorMessageResourceType = typeof(Resources))]
        public string ConfirmPassword { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Согласен с правилами проекта")]
        public bool RulesAgree { get; set; }
    }
}