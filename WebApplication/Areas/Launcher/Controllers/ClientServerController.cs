using System.Collections.Generic;

namespace WebApplication.Areas.Launcher.Controllers
{
    #region Using Directives

    using System;
    using System.Linq;
    using System.Data.Entity;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using Services;
    using WebApplication.Controllers;
    using Models.Json;

    #endregion

    public class ClientServerController : BaseController
    {
        /// <summary>
        /// Join server (../minecraft/join)
        /// </summary>
        /// <param name="selectedProfile">User Id (UUID)</param>
        /// <param name="accessToken">Session Id</param>
        /// <param name="serverId">Server Id</param>
        /// <returns></returns>
        [HttpPost]
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
                    id = playerSession.Md5,
                    name = playerSession.PlayerName
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
                var userSkinUrl = LauncherConfig.SkinsUrlString + playerSession.PlayerName;

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
        [HttpGet]
        public JsonResult PlayerProfile(string user)
        {
            try
            {
                var playerSession = this.context.PlayerSessions.First(ps => ps.Md5 == user);
                var unixTimeNow = ((int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
                var userSkinUrl = LauncherConfig.SkinsUrlString + playerSession.PlayerName;

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
        [HttpPost]
        public JsonResult UuidSkull(string username)
        {
            try
            {
                var playerSession = this.context.PlayerSessions.First(ps => ps.PlayerName == username);

                return Json(new JsonUserUuidData
                {
                    id = playerSession.Md5,
                    name = playerSession.PlayerName
                },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}