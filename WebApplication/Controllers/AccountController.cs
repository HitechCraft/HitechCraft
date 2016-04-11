using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Web.WebPages;

namespace WebApplication.Controllers
{
    #region Usings

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using WebApplication.Models;
    using AutoMapper;
    using WebApplication.Domain;

    #endregion

    [Authorize]
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ApplicationDbContext context = new ApplicationDbContext();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser user = null;

            if (model.EmailOrNickName.Contains('@'))
            {
                user = await UserManager.FindByEmailAsync(model.EmailOrNickName);
            }
            else
            {
                user = await UserManager.FindByNameAsync(model.EmailOrNickName);
            }

            if (user != null)
            {
                if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                {
                    ModelState.AddModelError("", string.Format(Resources.ErrorEmailNotConfirmed, user.Email));
                    return View(model);
                }
            }

            // Сбои при входе не приводят к блокированию учетной записи
            // Чтобы ошибки при вводе пароля инициировали блокирование учетной записи, замените на shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(user != null ? user.UserName : "", model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Неудачная попытка входа.");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Требовать предварительный вход пользователя с помощью имени пользователя и пароля или внешнего имени входа
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Приведенный ниже код защищает от атак методом подбора, направленных на двухфакторные коды. 
            // Если пользователь введет неправильные коды за указанное время, его учетная запись 
            // будет заблокирована на заданный период. 
            // Параметры блокирования учетных записей можно настроить в IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Неправильный код.");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //todo: выполнить проверку на наличие аккаунта, сравнение паролей старого и нового

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<RegisterViewModel, ApplicationUser>();
                var user = Mapper.Map<RegisterViewModel, ApplicationUser>(model);

                try
                {
                    CheckUserName(user.UserName);

                    //todo: реализовать ReCaptcha
                    var result = await UserManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        AddCurrency(user);

                        string token = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = token }, protocol: Request.Url.Scheme);
                        //todo: стилизовать письмо и засунуть в отдельный template
                        await UserManager.SendEmailAsync(user.Id, "Подтверждение учетной записи", "Подтвердите вашу учетную запись, щелкнув <a href=\"" + callbackUrl + "\">здесь</a>");

                        ViewBag.Email = user.Email;
                        return View("ConfirmEmailInfo");
                    }

                    AddErrors(result);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            // Появление этого сообщения означает наличие ошибки; повторное отображение формы
            return View(model);
        }

        private void AddCurrency(ApplicationUser user)
        {
            var currency = new Currency
            {
                username = user.UserName,
                user_id = user.Id,
                balance = this.DefaultUserGonts,
                realmoney = this.DefaultUserRubels,
                status = 0
            };

            context.Currencies.Add(currency);
            context.SaveChanges();
        }

        private void CheckUserName(string userName)
        {
            if (userName == "") throw new Exception(Resources.ErrorUserNameEmpty);

            var user = UserManager.FindByNameAsync(userName);

            if (user.Result != null)
            {
                throw new Exception(Resources.ErrorUserNameExists);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult IsUserExists(string userName)
        {
            try
            {
                CheckUserName(userName);

                return Json(new { message = Resources.UserNameNoExists, status = "success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message, status = "error" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UploadSkin()
        {
            var userId = User.Identity.GetUserId().ToString();
            UserSkin userSkin = GetSkinByUserId(userId);

            ViewBag.UserName = User.Identity.GetUserName();

            Mapper.CreateMap<UserSkin, UserSkinViewModel>()
                    .ForMember(dst => dst.Id, exp => exp.MapFrom(src => src.Id))
                    .ForMember(dst => dst.Image, exp => exp.MapFrom(src => src.Image))
                    .ForMember(dst => dst.UserId, exp => exp.MapFrom(src => src.UserId));

            var vm = Mapper.Map<UserSkin, UserSkinViewModel>(userSkin);

            return View(vm);
        }

        private UserSkin GetSkinByUserId(string userId)
        {
            ApplicationUser user = context.Users.First(u => u.Id == userId);
            UserSkin userSkin;

            try
            {
                userSkin = context.UserSkins.First(us => us.UserId == userId);
            }
            catch (Exception)
            {
                return GetSkinByGender(user.Gender);
            }

            return userSkin;
        }

        public UserSkin GetSkinByGender(Gender? gender)
        {
            string userId = "";

            switch (gender)
            {
                case Gender.Male:
                    userId = this.DefaultMaleUserId;
                    break;
                case Gender.Female:
                    userId = this.DefaultFemaleUserId;
                    break;
            }

            return context.UserSkins.First(us => us.UserId == userId);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetSkinImage(string userName, Gender? gender)
        {
            string userId;

            if (gender != null) return File(GetSkinByGender(gender).Image, "image/png");

            try
            {
                userId = context.Users.First(u => u.UserName == userName).Id;

                UserSkin userSkin = GetSkinByUserId(userId);

                return File(userSkin.Image, "image/png");
            }
            catch (Exception)
            {
                return File(this.GetSkinByUserId(this.DefaultMaleUserId).Image, "image/png");
            }
        }

        public void AddUserSkin(HttpPostedFileBase image)
        {
            var userSkin = new UserSkin();

            byte[] imageData = null;

            using (var binaryReader = new BinaryReader(image.InputStream))
            {
                imageData = binaryReader.ReadBytes(image.ContentLength);
            }

            userSkin.Image = imageData;

            userSkin.UserId = User.Identity.GetUserId();

            if (IsUserSkinExists(userSkin.UserId))
            {
                this.UserSkinUpdate(userSkin.UserId, userSkin.Image);
            }
            else
            {
                context.UserSkins.Add(userSkin);
                context.SaveChanges();
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult CheckSkinImage()
        {
            var uploadImage = Request.Files["uploadSkinImage"];

            var success = true;

            if (uploadImage == null || uploadImage.ContentType != "image/png")
            {
                ModelState.AddModelError("", Resources.ErrorSkinFormat);
                ModelState.AddModelError("", "Ошибка для теста");

                success = false;
            }
            else
            {
                this.AddUserSkin(uploadImage);
            }

            return Json(new { status = success, data = ModelState.Values });
        }

        private void UserSkinUpdate(string userId, byte[] newImage)
        {
            var userSkin = context.UserSkins.First(us => us.UserId == userId);

            userSkin.Image = newImage;

            context.Entry(userSkin).State = EntityState.Modified;
            context.SaveChanges();
        }

        private bool IsUserSkinExists(string userId)
        {
            try
            {
                context.UserSkins.First(us => us.UserId == userId);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Не показывать, что пользователь не существует или не подтвержден
                    return View("ForgotPasswordConfirmation");
                }

                // Дополнительные сведения о том, как включить подтверждение учетной записи и сброс пароля, см. по адресу: http://go.microsoft.com/fwlink/?LinkID=320771
                // Отправка сообщения электронной почты с этой ссылкой
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Сброс пароля", "Сбросьте ваш пароль, щелкнув <a href=\"" + callbackUrl + "\">здесь</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // Появление этого сообщения означает наличие ошибки; повторное отображение формы
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Не показывать, что пользователь не существует
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Создание и отправка маркера
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Выполнение входа пользователя посредством данного внешнего поставщика входа, если у пользователя уже есть имя входа
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // Если у пользователя нет учетной записи, то ему предлагается создать ее
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Получение сведений о пользователе от внешнего поставщика входа
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}