﻿namespace WebApplication.Controllers
{
    #region Using Directories

    using System.Data.Entity;
    using System.Web;
    using Microsoft.AspNet.Identity.Owin;
    using Domain;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Properties;
    using Managers;

    #endregion

    public class LauncherController : BaseController
    {
        #region Private Fields

        private ApplicationUserManager _userManager;

        #endregion
        
        #region Properties

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

                return Json(new
                {
                    status = "YES", message = Resources.LauncherSuccessAuth
                }, 
                JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "NO", message = Resources.LauncherErrorAuth
            }, 
            JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Check launcher version
        /// </summary>
        /// <param name="masterVersion">Launcher Master Version</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult CheckMasterVersion(string masterVersion)
        {
            if (LauncherManager.MasterVersion.Equals(masterVersion))
            {
                return Json(new
                {
                    status = "OK", message = Resources.LauncherValidVersion
                }, 
                JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = "NO", message = Resources.LauncherInvalidVersion
            }, 
            JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Check client structure
        /// </summary>
        /// <param name="clientName">name of selected client</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult CheckRequredFolders(string clientName)
        {
            var folders = LauncherManager.GetRequiredFolderList(clientName);

            foreach (string folder in folders)
            {
                if (!FileManager.IsDirOrFileExists(folder))
                {
                    return Json(new
                    {
                        status = "NO",
                        message = Resources.LauncherClientNoFolders
                    },
                        JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new
            {
                status = "YES", message = Resources.LauncherClientAllFolders
            }, 
            JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Join server (j)
        /// </summary>
        /// <param name="selectedProfile">User Id (UUID)</param>
        /// <param name="accessToken">Session Id</param>
        /// <param name="serverId">Server Id</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult JoinServer(string selectedProfile, string accessToken, string serverId)
        {
            return Json(new
            {
                error = "Bad login", errorMessage = "Bad login"
            }, 
            JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Check server (h)
        /// </summary>
        /// <param name="username">Player name</param>
        /// <param name="serverId">Server Id</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult CheckServer(string username, string serverId)
        {
            return Json(new
            {
                error = "Bad login", errorMessage = "Bad login"
            }, 
            JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string UuidConvert(string username)
        {
            return uuidFromString();
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
            var keyChars = LauncherManager.KeyChars;

            var random = new Random();

            for (int i = 0; i < LauncherManager.KeyLength; i++)
            {
                key += keyChars[random.Next(keyChars.Length)];
            }

            return key;
        }
        
        #endregion
    }
}