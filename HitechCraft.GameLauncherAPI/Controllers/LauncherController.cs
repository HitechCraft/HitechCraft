using HitechCraft.Core.Entity;
using HitechCraft.Core.Entity.Extentions;
using HitechCraft.Core.Helper;
using HitechCraft.Core.Models.Enum;
using HitechCraft.Core.Models.Json;
using HitechCraft.Core.Repository.Specification.PlayerSession;
using HitechCraft.Projector.Impl;

namespace HitechCraft.GameLauncherAPI.Controllers
{
    #region Using Directives

    using HitechCraft.Core.DI;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using BL.CQRS.Command;
    using BL.CQRS.Query;
    using Models;
    using Microsoft.AspNet.Identity.Owin;
    using Managers;

    #endregion

    public class LauncherController : BaseController
    {
        #region Private Fields

        private ApplicationUserManager _userManager;
        private ApplicationDbContext _context;

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

        public ApplicationDbContext Context
        {
            get
            {
                return _context ?? new ApplicationDbContext();
            }
            private set
            {
                _context = value;
            }
        }

        #endregion

        public LauncherController(IContainer container) : base(container)
        {
        }

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
            try
            {
                if (IsValidAuth(login, password))
                {
                    LogHelper.Info(Resource.SuccessAuth, "{User Checker}");

                    return Json(new JsonUserAuthData()
                    {
                        Status = JsonStatus.YES,
                        Message = Resource.SuccessAuth,
                        SessionData = GetUserSessionData(login)
                    },
                    JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                LogHelper.Error("Ошибка проверки данных пользователя: " + e.Message, "{User Checker}");

                return Json(new JsonUserAuthData()
                {
                    Status = JsonStatus.NO,
                    Message = "Ошибка проверки данных пользователя: " + e.Message
                },
                JsonRequestBehavior.AllowGet);
            }

            return Json(new JsonUserAuthData()
            {
                Status = JsonStatus.NO,
                Message = Resource.ErrorAuth
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
            try
            {
                if (Config.MasterVersion.Equals(masterVersion))
                {
                    LogHelper.Info(Resource.ValidVersion, "{Version Checker}");

                    return Json(new JsonStatusData()
                    {
                        Status = JsonStatus.YES,
                        Message = Resource.ValidVersion
                    },
                    JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                LogHelper.Error("Ошибка проверки версии лаунчера: " + e.Message, "{Version Checker}");

                return Json(new JsonStatusData()
                {
                    Status = JsonStatus.NO,
                    Message = "Ошибка проверки версии лаунчера: " + e.Message
                },
                JsonRequestBehavior.AllowGet);
            }

            LogHelper.Error(String.Format(Resource.InvalidVersion, Config.MasterVersion), "{Version Checker}");

            return Json(new JsonStatusData()
            {
                Status = JsonStatus.NO,
                Message = String.Format(Resource.InvalidVersion, Config.MasterVersion)
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
            try
            {
                var folders = FileManager.GetRequiredFolderList(clientName);

                foreach (string folder in folders)
                {
                    if (!FileManager.IsDirOrFileExists(folder))
                    {
                        LogHelper.Error(String.Format(Resource.ClientNoFolder, clientName, folder), "{Client Folders Checker}");

                        return Json(new JsonStatusData()
                        {
                            Status = JsonStatus.NO,
                            Message = String.Format(Resource.ClientNoFolder, clientName, folder)
                        },
                        JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.Error("Ошибка проверки обязательных папок клиента: " + e.Message, "{Client Folders Checker}");

                return Json(new JsonStatusData()
                {
                    Status = JsonStatus.NO,
                    Message = "Ошибка проверки обязательных папок клиента: " + e.Message
                },
                JsonRequestBehavior.AllowGet);
            }

            LogHelper.Info(Resource.ClientAllFolders, "{Client Folders Checker}");

            return Json(new JsonStatusData()
            {
                Status = JsonStatus.YES,
                Message = Resource.ClientAllFolders
            },
            JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// Returns Client file paths from server
        /// </summary>
        /// <param name="base64Hash">Base64 hash of client name</param>
        /// <returns></returns>
        public JsonResult GetJsonClientServerFilesData(string base64Hash)
        {
            List<JsonClientFilesData> clientFiles = new List<JsonClientFilesData>();

            var clientName = Encoding.UTF8.GetString(Convert.FromBase64String(base64Hash));

            var clientFolders = FileManager.GetRequiredFolderList(clientName);

            foreach (var folder in clientFolders)
            {
                if (FileManager.IsDirectory(folder))
                {
                    var files = FileManager.GetFiles(folder, "*", SearchOption.AllDirectories);

                    clientFiles.AddRange(files
                        .Select(x => new JsonClientFilesData()
                        {
                            FilePath = FileManager.GetClientFilePath(x, clientName),
                            HashSum = HashHelper.GetMd5Hash(System.IO.File.ReadAllBytes(x)),
                            FileSize = (int)new FileInfo(x).Length
                        }));
                }
            }
            
            return Json(new JsonClientData()
            {
                FilesData = clientFiles,
                ClientName = clientName
            },
            JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Feature for file download (from launcher client site folder)
        /// </summary>
        /// <param name="filePath">File Path with client name</param>
        public void DownloadClientFile(string filePath)
        {
            try
            {
                string serverClientPath = "\\Launcher\\Clients\\";

                string fileServerPath = (serverClientPath + filePath.Replace("/", "\\")).Replace("\\\\", "\\"); //fix

                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(fileServerPath));

                using (var fileContent = System.IO.File.Open(FileManager.GetServerPath(fileServerPath), FileMode.Open))
                {
                    Response.AppendHeader("Content-Length", fileContent.Length.ToString());
                    fileContent.Close();
                }

                Response.TransmitFile(fileServerPath);
                Response.End();
            }
            catch (Exception e)
            {
                LogHelper.Error("Ошибка скачивания файла: " + e.Message, "{Downloader}");
            }
        }
        
        /// <summary>
        /// Feature for file download (from launcher client site folder)
        /// </summary>
        /// <param name="filePath">File Path with client name</param>
        public JsonResult CheckJavaHash(string md5Hash, SystemBit systemBit)
        {
            try
            {
                var javaFullPath = FileManager.GetJavaPath(systemBit);

                var javaFile = javaFullPath + "/" + (systemBit == SystemBit.X64 ? "jre64.md5" : "jre32.md5");

                using (StreamReader sr = new StreamReader(System.IO.File.Open(javaFile, FileMode.Open)))
                {
                    if (sr.ReadToEnd() != md5Hash) throw new Exception("");
                }
            }
            catch (Exception e)
            {
                LogHelper.Error("Java не прошла проверку. " + e.Message, "{Java Checker}");

                return Json(new JsonStatusData()
                {
                    Status = JsonStatus.NO,
                    Message = "Java не прошла проверку. " + e.Message
                }, JsonRequestBehavior.AllowGet);
            }

            LogHelper.Info("Java прошла проверку", "{Java Checker}");

            return Json(new JsonStatusData()
            {
                Status = JsonStatus.YES,
                Message = "Java прошла проверку"
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Returns server info, from site context
        /// </summary>
        public JsonResult GetServersInfo()
        {
            try
            {
                var serversData = new ServerDataListQueryHandler(Container)
                .Handle(new ServerDataListQuery());

                if (!serversData.Any())
                {
                    return Json(new JsonMinecraftServersInfo()
                    {
                        ServerCount = 0
                    }, JsonRequestBehavior.AllowGet);
                }

                LogHelper.Info("Данные сервера получены", "{Server info}");

                return Json(new JsonMinecraftServersInfo()
                {
                    ServerData = serversData,
                    ServerCount = serversData.Count()
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                LogHelper.Error("Ошибка получения данных сервера: " + e.Message, "{Server info}");
            }

            return null;
        }

        /// <summary>
        /// Returns project news
        /// </summary>
        /// <returns></returns>
        public JsonResult GetLauncherNews()
        {
            try
            {
                var news = new EntityListQueryHandler<News, JsonLauncherNews>(Container)
                    .Handle(new EntityListQuery<News, JsonLauncherNews>()
                    {
                        Projector = Container.Resolve<IProjector<News, JsonLauncherNews>>()
                    }).OrderByDescending(x => x.Id).Limit(2);

                FixNewsTags(ref news);

                LogHelper.Info("Новости получены", "{News info}");

                return Json(news, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                LogHelper.Error("Ошибка получения новостей: " + e.Message, "{News info}");
            }

            return null;
        }
        
        #endregion

        #region Private Methods

        //TODO: Вынести новости в Html объект (что то типа браузера)
        private void FixNewsTags(ref IEnumerable<JsonLauncherNews> newsList)
        {
            foreach (var news in newsList)
            {
                news.Text = news.Text
                    .Replace("<p>", "")
                    .Replace("</p>", "")
                    .Replace("<strong>", "")
                    .Replace("</strong>", "")
                    .Replace("<ul>", "")
                    .Replace("</ul>", "")
                    .Replace("<li>", "")
                    .Replace("</li>", "");
            }
        }

        private bool IsValidAuth(string login, string password)
        {
            try
            {
                return UserManager.CheckPasswordAsync(this.Context.Users.First(u => u.UserName == login), password)
                        .Result;
            }
            catch (Exception e)
            {
                var message = e.Message;
                return false;
            }
        }

        private JsonSessionData GetUserSessionData(string login)
        {
            this.ChangeOrSetPlayerSession(login);

            try
            {
                var playerSession = new EntityListQueryHandler<PlayerSession, JsonSessionData>(Container)
                    .Handle(new EntityListQuery<PlayerSession, JsonSessionData>()
                    {
                        Specification = new PlayerSessionByPlayerNameSpec(login),
                        Projector = Container.Resolve<IProjector<PlayerSession, JsonSessionData>>()
                    }).First();

                return playerSession;
            }
            catch (Exception)
            {
                return new JsonSessionData();
            }
        }

        private void ChangeOrSetPlayerSession(string login)
        {
            var session = GenerateKey("Session", login);
            var token = GenerateKey("Token", login);

            try
            {
                var playerSession = new EntityListQueryHandler<PlayerSession, PlayerSessionModel>(Container)
                       .Handle(new EntityListQuery<PlayerSession, PlayerSessionModel>()
                       {
                           Specification = new PlayerSessionByPlayerNameSpec(login),
                           Projector = Container.Resolve<IProjector<PlayerSession, PlayerSessionModel>>()
                       }).First();

                playerSession.Session = session;
                playerSession.Token = token;

                this.CommandExecutor.Execute(Project<PlayerSessionModel, PlayerSessionUpdateCommand>(playerSession));
            }
            catch (Exception)
            {
                this.CommandExecutor.Execute(new PlayerSessionCreateCommand()
                {
                    PlayerName = login,
                    Server = null,
                    Session = session,
                    Token = token,
                    Md5 = UuidConvert(login)
                });
            }
        }

        private string UuidConvert(string username)
        {
            return HashHelper.StringFromUuid(HashHelper.UuidFromString("OfflinePlayer:" + username));
        }

        private string GenerateKey(string keyWord, string userName)
        {
            return UuidConvert(keyWord + userName + DateTime.Now);
        }
        
        #endregion
    }
}