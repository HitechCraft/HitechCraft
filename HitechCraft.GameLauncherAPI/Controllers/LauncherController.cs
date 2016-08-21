﻿using HitechCraft.GameLauncherAPI.Managers;
using HitechCraft.GameLauncherAPI.Properties;

namespace HitechCraft.GameLauncherAPI.Controllers
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using BL.CQRS.Command;
    using BL.CQRS.Query;
    using Common.Core;
    using Common.DI;
    using Common.Models.Enum;
    using Common.Models.Json.MinecraftLauncher;
    using Common.Models.Json.MinecraftServer;
    using Common.Projector;
    using DAL.Domain;
    using DAL.Domain.Extentions;
    using DAL.Repository.Specification;
    using Models;
    using Microsoft.AspNet.Identity.Owin;

    #endregion

    public class LauncherController : BaseController
    {
        public LauncherController(IContainer container) : base(container)
        {
        }


        #region Private Fields

        private ApplicationUserManager _userManager;
        private readonly ApplicationDbContext _context;

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

            if (IsValidAuth(login, password))
            {
                return Json(new JsonUserAuthData()
                {
                    Status = JsonStatus.YES,
                    Message = Resource.SuccessAuth,
                    SessionData = GetUserSessionData(login)
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
            if (LauncherConfig.MasterVersion.Equals(masterVersion))
            {
                return Json(new JsonStatusData()
                {
                    Status = JsonStatus.YES,
                    Message = Resource.ValidVersion
                },
                JsonRequestBehavior.AllowGet);
            }

            return Json(new JsonStatusData()
            {
                Status = JsonStatus.NO,
                Message = Resource.InvalidVersion
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
                if (!LauncherFileManager.IsDirOrFileExists(folder))
                {
                    return Json(new JsonStatusData()
                    {
                        Status = JsonStatus.NO,
                        Message = Resource.ClientNoFolders
                    },
                    JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new JsonStatusData()
            {
                Status = JsonStatus.YES,
                Message = Resource.ClientAllFolders
            },
            JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Check client files
        /// </summary>
        /// <param name="base64Hash">Base64 hash of clients file data</param>
        /// <returns></returns>
        public JsonResult CheckClientFiles(string base64Hash)
        {
            var errorFileList = new List<JsonErrorFileData>();

            try
            {
                var clientFilesData = Convert.FromBase64String(base64Hash);

                var filesFromClient = JsonSerializer.Deserialize<JsonClientData>(Encoding.UTF8.GetString(clientFilesData));

                var clientFolders = LauncherManager.GetRequiredFolderList(filesFromClient.ClientName);

                var filesFromApp = GetJsonClientFilesData(clientFolders, filesFromClient);

                errorFileList = GetErrorFileList(filesFromClient, filesFromApp, errorFileList);

                if (!errorFileList.Any())
                {
                    return Json(new JsonClientFilesStatusData()
                    {
                        Status = JsonStatus.YES,
                        Message = Resource.SuccessClientFilesCheck
                    }, JsonRequestBehavior.AllowGet);
                }

                return Json(new JsonClientFilesStatusData()
                {
                    FileData = errorFileList,
                    Status = JsonStatus.NO,
                    Message = Resource.ErrorClientFilesCheck
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new JsonClientFilesStatusData()
                {
                    FileData = errorFileList,
                    Status = JsonStatus.NO,
                    Message = e.Message
                }, JsonRequestBehavior.AllowGet);
            }
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

            var clientFolders = LauncherManager.GetRequiredFolderList(clientName);

            foreach (var folder in clientFolders)
            {
                if (LauncherFileManager.IsDirectory(folder))
                {
                    var files = LauncherFileManager.GetFiles(folder, "*", SearchOption.AllDirectories);

                    clientFiles.AddRange(files
                        .Select(x => new JsonClientFilesData()
                        {
                            FilePath = LauncherFileManager.GetClientFilePath(x, clientName),
                            HashSum = HashManager.GetMd5Hash(System.IO.File.ReadAllBytes(x)),
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
            string serverClientPath = "\\Areas\\Launcher\\Clients\\";

            string fileServerPath = (serverClientPath + filePath.Replace("/", "\\")).Replace("\\\\", "\\"); //fix

            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(fileServerPath));

            using (var fileContent = System.IO.File.Open(LauncherFileManager.GetServerPath(fileServerPath), FileMode.Open))
            {
                Response.AddHeader("Content-Length", fileContent.Length.ToString());
                fileContent.Close();
            }

            Response.TransmitFile(fileServerPath);
            Response.End();
        }

        /// <summary>
        /// Returns server info, from site context
        /// </summary>
        public JsonResult GetServersInfo()
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

            return Json(new JsonMinecraftServersInfo()
            {
                ServerData = serversData,
                ServerCount = serversData.Count()
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Returns project news
        /// </summary>
        /// <returns></returns>
        public JsonResult GetLauncherNews()
        {
            var news = new EntityListQueryHandler<News, JsonLauncherNews>(Container)
                .Handle(new EntityListQuery<News, JsonLauncherNews>()
                {
                    Projector = Container.Resolve<IProjector<News, JsonLauncherNews>>()
                }).OrderByDescending(x => x.Id).Limit(2);

            FixNewsTags(ref news);

            return Json(news, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Private Methods

        private void FixNewsTags(ref IEnumerable<JsonLauncherNews> newsList)
        {
            foreach (var news in newsList)
            {
                news.Text = news.Text
                    .Replace("[p]", "")
                    .Replace("[/p]", "")
                    .Replace("[b]", "")
                    .Replace("[/b]", "")
                    .Replace("[ul]", "")
                    .Replace("[/ul]", "")
                    .Replace("[li]", "")
                    .Replace("[/li]", "");
            }
        }

        private bool IsValidAuth(string login, string password)
        {
            try
            {
                return UserManager.CheckPasswordAsync(_context.Users.First(u => u.UserName == login), password)
                        .Result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private JsonSessionData GetUserSessionData(string login)
        {
            ChangeOrSetPlayerSession(login);

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
                var playerSession = new EntityListQueryHandler<PlayerSession, PlayerSessionEditViewModel>(Container)
                       .Handle(new EntityListQuery<PlayerSession, PlayerSessionEditViewModel>()
                       {
                           Specification = new PlayerSessionByPlayerNameSpec(login),
                           Projector = Container.Resolve<IProjector<PlayerSession, PlayerSessionEditViewModel>>()
                       }).First();

                playerSession.Session = session;
                playerSession.Token = token;

                CommandExecutor.Execute(Project<PlayerSessionEditViewModel, PlayerSessionUpdateCommand>(playerSession));
            }
            catch (Exception)
            {
                CommandExecutor.Execute(new PlayerSessionCreateCommand()
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
            return HashManager.StringFromUuid(HashManager.UuidFromString("OfflinePlayer:" + username));
        }

        private string GenerateKey(string keyWord, string userName)
        {
            return UuidConvert(keyWord + userName + DateTime.Now);
        }

        private List<JsonClientFilesData> GetJsonClientFilesData(List<string> clientFolders, JsonClientData filesData)
        {
            List<JsonClientFilesData> clientFiles = new List<JsonClientFilesData>();

            foreach (var folder in clientFolders)
            {
                if (LauncherFileManager.IsDirectory(folder))
                {
                    var files = LauncherFileManager.GetFiles(folder, "*", SearchOption.AllDirectories);

                    clientFiles.AddRange(files
                        .Select(x => new JsonClientFilesData()
                        {
                            FilePath = LauncherFileManager.GetClientFilePath(x, filesData.ClientName),
                            HashSum = HashManager.GetMd5Hash(System.IO.File.ReadAllBytes(x))
                        }));
                }
            }

            return clientFiles;
        }

        private List<JsonErrorFileData> GetErrorFileList(JsonClientData filesFromClient, List<JsonClientFilesData> filesFromApp, List<JsonErrorFileData> errorFileList)
        {
            var fixedFiles = FixPathString(filesFromClient.FilesData);

            //Check client files contains on server (if contains - check hashsum)
            foreach (var clientFile in fixedFiles)
            {
                if (filesFromApp.Any(f => f.FilePath == clientFile.FilePath))
                {
                    var existingFile = filesFromApp.First(f => f.FilePath == clientFile.FilePath);

                    if (existingFile.HashSum != clientFile.HashSum)
                    {
                        errorFileList.Add(new JsonErrorFileData()
                        {
                            FilePath = clientFile.FilePath,
                            FileAction = FileAction.Reload
                        });
                    }

                    filesFromApp.Remove(existingFile);
                }
                else
                {
                    errorFileList.Add(new JsonErrorFileData()
                    {
                        FilePath = clientFile.FilePath,
                        FileAction = FileAction.Remove
                    });
                }
            }

            //Add remaining files to upload
            if (filesFromApp.Any())
                errorFileList.AddRange(filesFromApp.Select(f => new JsonErrorFileData()
                {
                    FilePath = f.FilePath,
                    FileAction = FileAction.Load
                }));

            return errorFileList;
        }

        private IEnumerable<JsonClientFilesData> FixPathString(ICollection<JsonClientFilesData> filesData)
        {
            return filesData.Select(fd => new JsonClientFilesData()
            {
                FilePath = fd.FilePath.Replace("\\", "/"),
                HashSum = fd.HashSum
            });
        }

        #endregion
    }
}