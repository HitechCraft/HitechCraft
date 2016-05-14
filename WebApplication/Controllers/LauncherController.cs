using System.Data.Entity;
using Microsoft.Ajax.Utilities;
using WebApplication.Domain;

namespace WebApplication.Controllers
{
    #region Using Directories

    using System;
    using System.Linq;
    using System.Web.Mvc;

    #endregion
    
    public class LauncherController : AccountController
    {
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

        #endregion

        #region Launcher Actions

        public JsonResult CheckPlayerData(string login, string password)
        {
            if (IsValidAuth(login, password))
            {
                this.ChangeOrSetPlayerSession(login);

                return Json(new { status = "YES", message = "Успешная авторизация" });
            }

            return Json(new { status = "NO", message = "Неверные данные" });
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

            for (int i = 0; i < this.KeyLength; i++)
            {
                key += this.KeyChars[new Random().Next(0, this.KeyLength - 1)];
            }

            return key;
        }

        #endregion
    }
}