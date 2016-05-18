namespace WebApplication.Areas.Launcher.Controllers
{
    #region Using Directives

    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using Microsoft.AspNet.Identity.Owin;
    using WebApplication.Controllers;
    using Domain;
    using Managers;
    using Properties;
    using Services;
    using Models.Json;

    #endregion

    public class ActionController : BaseController
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

        #region Actions

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

            if (this.IsValidAuth(login, password))
            {
                return Json(new JsonUserAuthData()
                {
                    Status = JsonStatus.YES,
                    Message = Resources.LauncherSuccessAuth,
                    SessionData = this.GetUserSessionData(login)
                },
                JsonRequestBehavior.AllowGet);
            }

            return Json(new JsonUserAuthData()
            {
                Status = JsonStatus.YES,
                Message = Resources.LauncherErrorAuth
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
            if (LauncherConfig.MasterVersion.Equals(masterVersion))
            {
                return Json(new JsonStatusData()
                {
                    Status = JsonStatus.YES,
                    Message = Resources.LauncherValidVersion
                },
                JsonRequestBehavior.AllowGet);
            }

            return Json(new JsonStatusData()
            {
                Status = JsonStatus.NO,
                Message = Resources.LauncherInvalidVersion
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
                    return Json(new JsonStatusData()
                    {
                        Status = JsonStatus.NO,
                        Message = Resources.LauncherClientNoFolders
                    },
                    JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new JsonStatusData()
            {
                Status = JsonStatus.YES,
                Message = Resources.LauncherClientAllFolders
            },
            JsonRequestBehavior.AllowGet);
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

        private JsonSessionData GetUserSessionData(string login)
        {
            this.ChangeOrSetPlayerSession(login);

            try
            {
                PlayerSession playerSession = this.context.PlayerSessions.First(ps => ps.PlayerName == login);

                Mapper.CreateMap<PlayerSession, JsonSessionData>()
                        .ForMember(dst => dst.PlayerName, exp => exp.MapFrom(src => src.PlayerName))
                        .ForMember(dst => dst.Md5, exp => exp.MapFrom(src => src.Md5))
                        .ForMember(dst => dst.ServerId, exp => exp.MapFrom(src => src.Server))
                        .ForMember(dst => dst.SessionId, exp => exp.MapFrom(src => src.Session))
                        .ForMember(dst => dst.Token, exp => exp.MapFrom(src => src.Token));

                return Mapper.Map<PlayerSession, JsonSessionData>(playerSession);
            }
            catch (Exception)
            {
                return new JsonSessionData();
            }
        }

        private void ChangeOrSetPlayerSession(string login)
        {
            var session = this.GenerateKey("Session", login);
            var token = this.GenerateKey("Token", login);

            try
            {
                var playerSession = this.context.PlayerSessions.First(ps => ps.PlayerName == login);

                playerSession.Session = session;
                playerSession.Token = token;

                this.context.Entry(playerSession).State = EntityState.Modified;
                this.context.SaveChanges();
            }
            catch (Exception)
            {
                this.context.PlayerSessions.Add(new PlayerSession
                {
                    PlayerName = login,
                    Session = session,
                    Server = null,
                    Token = token,
                    Md5 = this.UuidConvert(login)
                });

                this.context.SaveChanges();
            }
        }

        private string UuidConvert(string username)
        {
            return Md5Manager.StringFromUuid(Md5Manager.UuidFromString("OfflinePlayer:" + username));
        }

        private string GenerateKey(string keyWord, string userName)
        {
            return this.UuidConvert(keyWord + userName + DateTime.Now);
        }

        #endregion
    }
}