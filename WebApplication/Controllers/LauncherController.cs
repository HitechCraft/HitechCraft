namespace WebApplication.Controllers
{
    #region Using Directories

    using System.Data.Entity;
    using System.Web;
    using Microsoft.AspNet.Identity.Owin;
    using Domain;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    #endregion
    
    public class LauncherController : BaseController
    {
        #region Private Fields

        private ApplicationUserManager _userManager;

        #endregion
        
        #region Properties

        public int KeyLength
        {
            get { return 64; } 
            protected set { }
        }

        public string KeyChars
        {
            get { return "abcdefghijklmnopqrstuvwxyz1234567890"; }
            protected set { }
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

        #endregion

        #region Launcher Actions

        /// <summary>
        /// Player atuh cheking
        /// </summary>
        /// <param name="login">Player login</param>
        /// <param name="password">Player password</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult CheckPlayerData(string login, string password)
        {
            //TODO: сделать отправку пароля хэшем

            if (IsValidAuth(login, password))
            {
                this.ChangeOrSetPlayerSession(login);

                return Json(new { status = "YES", message = "Успешная авторизация" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "NO", message = "Неверные данные" }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        
        #region Private Methods

        private bool IsValidAuth(string login, string password)
        {
            try
            {
                var isValid = this.UserManager.CheckPasswordAsync(this.context.Users.First(u => u.UserName == login), password).Result;

                return isValid;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void ChangeOrSetPlayerSession(string login)
        {
            try
            {
                var playerSession = this.context.PlayerSessions.First(ps => ps.PlayerName == login);

                playerSession.Session = this.GenerateKey();
                playerSession.Token = this.GenerateKey();

                this.context.Entry(playerSession).State = EntityState.Modified;
                this.context.SaveChanges();
            }
            catch (Exception)
            {
                this.context.PlayerSessions.Add(new PlayerSession
                {
                    PlayerName = login,
                    Session = this.GenerateKey(),
                    Server = null,
                    Token = this.GenerateKey()
                });

                this.context.SaveChanges();
            }
        }

        private string GenerateKey()
        {
            var key = String.Empty;
            var random = new Random();

            for (int i = 0; i < this.KeyLength; i++)
            {
                key += this.KeyChars[random.Next(this.KeyChars.Length)];
            }

            return key;
        }
        
        #endregion
    }
}