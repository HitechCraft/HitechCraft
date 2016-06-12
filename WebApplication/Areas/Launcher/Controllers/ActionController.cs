namespace WebApplication.Areas.Launcher.Controllers
{
    #region Using Directives

    using System.Collections.Generic;
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
    using System.IO;
    using Core;

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
                Status = JsonStatus.NO,
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
        
        /// <summary>
        /// Check client files
        /// </summary>
        /// <param name="clientFilesData">Files data from client</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult CheckClientFiles(string clientFilesData)
        {
            var errorFileList = new List<JsonErrorFileData>();

            try
            {
                var filesFromClient = JsonManager.Deserialize<JsonClientData>(clientFilesData);

                var clientFolders = LauncherManager.GetRequiredFolderList(filesFromClient.ClientName);

                var filesFromApp = this.GetJsonClientFilesData(clientFolders, filesFromClient);

                errorFileList = this.GetErrorFileList(filesFromClient, filesFromApp, errorFileList);

                if (!errorFileList.Any())
                {
                    return Json(new JsonClientFilesStatusData()
                    {
                        Status = JsonStatus.YES,
                        Message = Resources.SuccessClientFilesCheck
                    }, JsonRequestBehavior.AllowGet);
                }
                
                return Json(new JsonClientFilesStatusData()
                {
                    FileData = errorFileList,
                    Status = JsonStatus.NO,
                    Message = Resources.ErrorClientFilesCheck
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
                            HashSum = Md5Manager.GetMd5Hash(new FileStream(x, FileMode.Create))
                        }));
                }
            }

            return clientFiles;
        }

        private List<JsonErrorFileData> GetErrorFileList(JsonClientData filesFromClient, List<JsonClientFilesData> filesFromApp, List<JsonErrorFileData> errorFileList)
        {
            //Check client files contains on server (if contains - check hashsum)
            foreach (var clientFile in filesFromClient.FilesData)
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

        #endregion
    }
}