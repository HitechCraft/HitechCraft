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
            if (Config.MasterVersion.Equals(masterVersion))
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
            var folders = FileManager.GetRequiredFolderList(clientName);

            foreach (string folder in folders)
            {
                if (!FileManager.IsDirOrFileExists(folder))
                {
                    return Json(new JsonStatusData()
                    {
                        Status = JsonStatus.NO,
                        Message = String.Format(Resource.ClientNoFolder, clientName, folder)
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

        /// <summary>
        /// Feature for file download (from launcher client site folder)
        /// </summary>
        /// <param name="filePath">File Path with client name</param>
        public void DownloadJava(SystemBit systemBit)
        {
            var javaFullPath = FileManager.GetJavaPath(systemBit);

            var javaFile = javaFullPath + "/" + (systemBit == SystemBit.X64 ? "jre64.tar.gz" : "jre32.tar.gz");

            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(javaFile));

            using (var fileContent = System.IO.File.Open(javaFile, FileMode.Open))
            {
                Response.AppendHeader("Content-Length", fileContent.Length.ToString());
            }

            Response.TransmitFile(javaFile);
            Response.End();
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
                return Json(new JsonStatusData()
                {
                    Status = JsonStatus.NO,
                    Message = "Java не прошла проверку. " + e.Message
                }, JsonRequestBehavior.AllowGet);
            }

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
                if (FileManager.IsDirectory(folder))
                {
                    var files = FileManager.GetFiles(folder, "*", SearchOption.AllDirectories);

                    clientFiles.AddRange(files
                        .Select(x => new JsonClientFilesData()
                        {
                            FilePath = FileManager.GetClientFilePath(x, filesData.ClientName),
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