namespace HitechCraft.GameLauncherAPI.Controllers
{
    #region Using Directives

    using System.Web.Mvc;
    using Common.DI;
    using System;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Script.Serialization;
    using BL.CQRS.Command;
    using BL.CQRS.Query;
    using Common.Core;
    using Common.Models.Json.MinecraftClient;
    using Common.Projector;
    using DAL.Domain;
    using DAL.Repository.Specification;
    using Managers;
    using Models;


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
                var playerSession = new EntityListQueryHandler<PlayerSession, PlayerSessionModel>(Container)
                    .Handle(new EntityListQuery<PlayerSession, PlayerSessionModel>()
                    {
                        Specification = new PlayerSessionByPlayerProfileSpec(selectedProfile) & new PlayerSessionByAccessTokenSpec(accessToken),
                        Projector = Container.Resolve<IProjector<PlayerSession, PlayerSessionModel>>()
                    }).First();

                playerSession.Server = serverId;

                this.CommandExecutor.Execute(this.Project<PlayerSessionModel, PlayerSessionUpdateCommand>(playerSession));

                return Json(new
                {
                    id = playerSession.Md5,
                    name = playerSession.PlayerName
                },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
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
                var playerSession = new EntityListQueryHandler<PlayerSession, PlayerSessionModel>(Container)
                    .Handle(new EntityListQuery<PlayerSession, PlayerSessionModel>()
                    {
                        Specification = new PlayerSessionByPlayerNameSpec(username) & new PlayerSessionByServerSpec(serverId),
                        Projector = Container.Resolve<IProjector<PlayerSession, PlayerSessionModel>>()
                    }).First();

                var unixTimeNow = ((int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
                var userSkinUrl = Config.SkinsUrlString + "/" + playerSession.PlayerName + ".png";

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
                LogManager.Error("Ошибка передачи параметров серверу: " + e.Message, "CheckServer");

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
            if (!Regex.IsMatch(playerName, "/[A-Za-z0-9-_]+.png")) playerName = "/" + playerName + ".png";

            //Minecraft кэширует скины, имя файла строит по первым 2м буквам 
            //от имени url после последнего слэша
            //Поэтому вставил вот такой костыль, извините уж
            playerName = playerName.Split('/')[1].Split('.')[0];

            var playerSkinVm = new PlayerSkinQueryHandler<PlayerSkinModel>(Container)
                .Handle(new PlayerSkinQuery<PlayerSkinModel>()
                {
                    UserName = playerName,
                    Projector = Container.Resolve<IProjector<PlayerSkin, PlayerSkinModel>>()
                });

            return File(playerSkinVm.Image, "image/png");
        }

        [HttpGet]
        public ActionResult GetDynmapSkinImage(string playerName)
        {
            var playerSkinVm = new PlayerSkinQueryHandler<PlayerSkinModel>(Container)
                .Handle(new PlayerSkinQuery<PlayerSkinModel>()
                {
                    UserName = playerName,
                    Projector = Container.Resolve<IProjector<PlayerSkin, PlayerSkinModel>>()
                });

            return File(SkinManager.ScaleSkinToDefaultMCSize(playerSkinVm.Image), "image/png");
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
                var playerSession = new EntityListQueryHandler<PlayerSession, PlayerSessionModel>(Container)
                    .Handle(new EntityListQuery<PlayerSession, PlayerSessionModel>()
                    {
                        Specification = new PlayerSessionByPlayerProfileSpec(user),
                        Projector = Container.Resolve<IProjector<PlayerSession, PlayerSessionModel>>()
                    }).First();

                var unixTimeNow = ((int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
                var userSkinUrl = Config.SkinsUrlString + "/" + playerSession.PlayerName + ".png";

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
                LogManager.Error("Ошибка получения профиля игрока." + e.Message, "PlayerProfile");

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
                var playerSession = new EntityListQueryHandler<PlayerSession, PlayerSessionModel>(Container)
                    .Handle(new EntityListQuery<PlayerSession, PlayerSessionModel>()
                    {
                        Specification = new PlayerSessionByPlayerNameSpec(username),
                        Projector = Container.Resolve<IProjector<PlayerSession, PlayerSessionModel>>()
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