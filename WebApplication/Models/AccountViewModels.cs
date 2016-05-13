namespace WebApplication.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Domain;
    using Properties;

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(Resources))]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Код")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Запомнить браузер?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
    }

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
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Почта")]
        public string Email { get; set; }
    }
}
