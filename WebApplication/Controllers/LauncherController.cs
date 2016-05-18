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
    using Properties;
    using Managers;
    using System.Text;
    using System.Web.Script.Serialization;
    using Domain.Json;

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

                return Json(new JsonStatusData()
                {
                    Status = JsonStatus.YES,
                    Message = Resources.LauncherSuccessAuth
                },
                JsonRequestBehavior.AllowGet);
            }

            return Json(new JsonStatusData()
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
            if (LauncherManager.MasterVersion.Equals(masterVersion))
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

        #region Client - Server Actions
        
        /// <summary>
        /// Join server (../minecraft/join)
        /// </summary>
        /// <param name="selectedProfile">User Id (UUID)</param>
        /// <param name="accessToken">Session Id</param>
        /// <param name="serverId">Server Id</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult JoinServer(string selectedProfile, string accessToken, string serverId)
        {
            try
            {
                var playerSession =
                    this.context.PlayerSessions.First(ps => ps.Md5 == selectedProfile && ps.Session == accessToken);

                playerSession.Server = serverId;

                this.context.Entry(playerSession).State = EntityState.Modified;
                this.context.SaveChanges();

                return Json(new
                {
                    id = playerSession.Md5, name = playerSession.PlayerName
                }, 
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonErrorData
                {
                    error = "Bad login",
                    errorMessage = "Bad login"
                },
                JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Check server (../minecraft/hasJoined)
        /// </summary>
        /// <param name="username">>Player nickname</param>
        /// <param name="serverId">Server Id</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult CheckServer(string username, string serverId)
        {
            try
            {
                var playerSession = this.context.PlayerSessions.First(ps => ps.PlayerName == username && ps.Server == serverId);
                var unixTimeNow = ((int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
                var userSkinUrl = LauncherManager.SkinsUrlString + playerSession.PlayerName;

                var userData = Json(new JsonClientUserData
                {
                    timestamp = unixTimeNow,
                    profileId = playerSession.Md5,
                    profileName = playerSession.PlayerName,
                    textures = new JsonTextureData
                    {
                        SKIN = new JsonUserSkinData
                        {
                            url = userSkinUrl
                        } 
                    }
                }, JsonRequestBehavior.AllowGet);

                return Json(new JsonClientResponseData
                {
                    id = playerSession.Md5,
                    name = playerSession.PlayerName,
                    properties = new JsonClientPropertiesData[]
                    {
                        new JsonClientPropertiesData
                        {
                            name = "textures",
                            value = Convert.ToBase64String(Encoding.ASCII.GetBytes(new JavaScriptSerializer().Serialize(userData.Data))),
                            signature = "Cg=="
                        }
                    }
                },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonErrorData
                {
                    error = "Bad login",
                    errorMessage = "Bad login"
                },
                JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Returns player data (../minecraft/profile/)
        /// </summary>
        /// <param name="user">Player md5 hash</param>
        /// <returns></returns>
        public JsonResult PlayerProfile(string user)
        {
            try
            {
                var playerSession = this.context.PlayerSessions.First(ps => ps.Md5 == user);
                var unixTimeNow = ((int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
                var userSkinUrl = LauncherManager.SkinsUrlString + playerSession.PlayerName;

                var userData = Json(new JsonClientUserData
                {
                    timestamp = unixTimeNow,
                    profileId = playerSession.Md5,
                    profileName = playerSession.PlayerName,
                    textures = new JsonTextureData
                    {
                        SKIN = new JsonUserSkinData
                        {
                            url = userSkinUrl
                        }
                    }
                }, JsonRequestBehavior.AllowGet);

                return Json(new JsonClientResponseData
                {
                    id = playerSession.Md5,
                    name = playerSession.PlayerName,
                    properties = new JsonClientPropertiesData[]
                    {
                        new JsonClientPropertiesData
                        {
                            name = "textures",
                            value = Convert.ToBase64String(Encoding.ASCII.GetBytes(new JavaScriptSerializer().Serialize(userData.Data))),
                            signature = ""
                        }
                    }
                },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Player skull fix
        /// </summary>
        /// <param name="username">Player nickname</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult UuidSkull(string username)
        {
            try
            {
                var playerSession = this.context.PlayerSessions.First(ps => ps.PlayerName == username);

                return Json(new
                {
                    id = playerSession.Md5, name = playerSession.PlayerName
                },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return null;
            }
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
                    Token = this.GenerateKey(),
                    Md5 = this.UuidConvert(login)
                });

                this.context.SaveChanges();
            }
        }

        private string UuidConvert(string username)
        {
            return Md5Manager.StringFromUuid(Md5Manager.UuidFromString("OfflinePlayer:" + username));
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