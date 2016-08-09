namespace HitechCraft.WebApplication.Areas.Launcher.Controllers
{
    #region Using Directives

    using System;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using WebApplication.Controllers;
    using Common.Models.Json.MinecraftClient;
    using Services;
    using Common.DI;
    using System.Linq;
    using BL.CQRS.Query;
    using Common.Projector;
    using DAL.Domain;
    using DAL.Repository.Specification;
    using Models;
    using BL.CQRS.Command;
    using Common.Core;

    #endregion

    public class ClientServerController : BaseController
    {
        public ClientServerController(IContainer container) : base(container)
        {
        }

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
                LogManager.Info("[Joinserver] Полученные параметры: profile " + selectedProfile
                    + ", token: " + accessToken
                    + ", server session: " + serverId);

                var playerSession = new EntityListQueryHandler<PlayerSession, PlayerSessionEditViewModel>(this.Container)
                    .Handle(new EntityListQuery<PlayerSession, PlayerSessionEditViewModel>()
                    {
                        Specification = new PlayerSessionByPlayerProfileSpec(selectedProfile) & new PlayerSessionByAccessTokenSpec(accessToken),
                        Projector = this.Container.Resolve<IProjector<PlayerSession, PlayerSessionEditViewModel>>()
                    }).First();
                
                playerSession.Server = serverId;

                this.CommandExecutor.Execute(this.Project<PlayerSessionEditViewModel, PlayerSessionUpdateCommand>(playerSession));

                LogManager.Info("[Joinserver] Переданные параметры игрока "
                    + playerSession.PlayerName + " (" + selectedProfile + "): " +
                    "ceccия клиента" + playerSession +
                    ", сессия сервера " + serverId +
                    ", md5 хэш " + playerSession.Md5);

                return Json(new
                {
                    id = playerSession.Md5,
                    name = playerSession.PlayerName
                },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                LogManager.Error("[Joinserver] Ошибка передачи параметров клиенту: " + e.Message);

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
                LogManager.Info("[Checkserver] Полученные параметры: User Name " + username
                    + ", server session: " + serverId);

                var playerSession = new EntityListQueryHandler<PlayerSession, PlayerSessionEditViewModel>(this.Container)
                    .Handle(new EntityListQuery<PlayerSession, PlayerSessionEditViewModel>()
                    {
                        Specification = new PlayerSessionByPlayerNameSpec(username) & new PlayerSessionByServerSpec(serverId),
                        Projector = this.Container.Resolve<IProjector<PlayerSession, PlayerSessionEditViewModel>>()
                    }).First();

                var unixTimeNow = ((int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
                var userSkinUrl = LauncherConfig.SkinsUrlString + playerSession.PlayerName;

                LogManager.Info("[Checkserver] Переданные параметры игрока "
                    + playerSession.PlayerName + " (" + playerSession.Md5 + "): " +
                    " скин игрока " + userSkinUrl +
                    ", md5 хэш " + playerSession.Md5);

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
            catch (Exception e)
            {
                LogManager.Error("[Checkserevr] Ошибка передачи параметров серверу: " + e.Message);

                return Json(new JsonErrorData
                {
                    error = "Bad login",
                    errorMessage = "Bad login"
                },
                JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpGet]
        public ActionResult GetSkinImage(string playerName)
        {
            //Minecraft кэширует скины, имя файла строит по первым 2м буквам 
            //от имени url после последнего слэша
            //Поэтому вставил вот такой костыль, извините уж
            playerName = playerName.Split('/')[1].Split('.')[0];

            LogManager.Info("Попытка получить скин " + playerName, "MinecraftSkinGet");

            var playerSkinVm = new PlayerSkinQueryHandler<PlayerSkinViewModel>(this.Container)
                .Handle(new PlayerSkinQuery<PlayerSkinViewModel>()
                {
                    UserName = playerName,
                    Projector = this.Container.Resolve<IProjector<PlayerSkin, PlayerSkinViewModel>>()
                });

            LogManager.Info((playerSkinVm != null) + " " + (playerSkinVm.Image != null) + " Скин получен " + playerName, "MinecraftSkinGet");

            return File(playerSkinVm.Image, "image/png");
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
                LogManager.Info("Получение профиля игрока " + user, "PlayerProfile");

                var playerSession = new EntityListQueryHandler<PlayerSession, PlayerSessionEditViewModel>(this.Container)
                    .Handle(new EntityListQuery<PlayerSession, PlayerSessionEditViewModel>()
                    {
                        Specification = new PlayerSessionByPlayerProfileSpec(user),
                        Projector = this.Container.Resolve<IProjector<PlayerSession, PlayerSessionEditViewModel>>()
                    }).First();
                
                var unixTimeNow = ((int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
                var userSkinUrl = LauncherConfig.SkinsUrlString + "/" + playerSession.PlayerName + ".png";

                LogManager.Info("Скин Url " + userSkinUrl, "PlayerProfile");

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
            catch (Exception e)
            {
                LogManager.Info("Ошибка получения профиля игрока." + e.Message, "PlayerProfile");

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
                var playerSession = new EntityListQueryHandler<PlayerSession, PlayerSessionEditViewModel>(this.Container)
                    .Handle(new EntityListQuery<PlayerSession, PlayerSessionEditViewModel>()
                    {
                        Specification = new PlayerSessionByPlayerNameSpec(username),
                        Projector = this.Container.Resolve<IProjector<PlayerSession, PlayerSessionEditViewModel>>()
                    }).First();

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