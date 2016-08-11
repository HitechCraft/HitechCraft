using System.Text;
using HitechCraft.Common.Core;

namespace HitechCraft.WebApplication.Controllers
{
    #region Using Directives

    using Common.DI;
    using System;
    using System.Web.Mvc;
    using DAL.Domain;
    using Models;
    using BL.CQRS.Query;
    using Properties;
    using System.Text.RegularExpressions;
    using Common.Projector;
    using BL.CQRS.Command;
    using Manager;
    using Common.Models.Enum;
    using System.Linq;
    using DAL.Domain.Extentions;
    using DAL.Repository.Specification;
    using System.Collections.Generic;
    using System.Web;

    #endregion

    [Authorize]
    public class PlayerController : BaseController
    {
        #region Constructor

        public PlayerController(IContainer container) : base(container)
        {
        }

        #endregion

        #region Player Actions

        public new ActionResult Profile()
        {
            try
            {
                var vm = this.Project<Player, PlayerProfileViewModel>(this.Player);

                ViewBag.Gonts = this.Currency.Gonts;
                ViewBag.Rubels = this.Currency.Rubels;

                return View(vm);
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult IsUserExists(string userName)
        {
            try
            {
                this.CheckUserName(userName);

                return Json(new { message = Resources.UserNameNoExists, status = "success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message, status = "error" }, JsonRequestBehavior.AllowGet);
            }
        }
        
        [AllowAnonymous]
        [HttpPost]
        public JsonResult UploadSkinImage()
        {
            var uploadImage = Request.Files["uploadSkinImage"];

            byte[] bytes;

            var errors = this.CheckPlayerSkin(uploadImage, out bytes);
            
            if (!errors.Any())
            {
                this.CommandExecutor.Execute(new PlayerSkinCreateOrUpdateCommand()
                {
                    PlayerId = this.Player.Id,
                    PlayerName = this.Player.Name,
                    Image = bytes
                });

                return Json(new { status = "OK", data = "" });
            }
            else
            {
                return Json(new { status = "NO", data = errors });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetSkinImage(Gender? gender, string playerName = "")
        {
            var playerSkinVm = new PlayerSkinQueryHandler<PlayerSkinViewModel>(this.Container)
                .Handle(new PlayerSkinQuery<PlayerSkinViewModel>()
                {
                    UserName = playerName != "" ? playerName : (this.Player != null ? this.Player.Name : ""),
                    Gender = gender != null ? gender.Value : (this.Player != null ? this.Player.Gender : Gender.Male),
                    Projector = this.Container.Resolve<IProjector<PlayerSkin, PlayerSkinViewModel>>()
                });

            return File(playerSkinVm.Image, "image/png");
        }

        public bool IsPlayerSkinExists()
        {
            return new PlayerSkinExistsQueryHandler(this.Container)
                .Handle(new PlayerSkinExistsQuery()
                {
                    UserName = this.Player.Name
                });
        }

        [HttpPost]
        public JsonResult RemovePlayerSkin()
        {
            try
            {
                if (!IsPlayerSkinExists())
                {
                    return Json(new
                    {
                        status = JsonStatus.NO,
                        message = "Скин пользователя не загружен"
                    }, JsonRequestBehavior.AllowGet);
                }

                new PlayerSkinRemoveCommandHandler(this.Container)
                    .Handle(new PlayerSkinRemoveCommand()
                    {
                        PlayerName = this.Player.Name
                    });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = JsonStatus.NO,
                    message = e.Message
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = JsonStatus.YES,
                message = "Скин успешно сменен на стандартный"
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Security()
        {
            var vm = new EntityListQueryHandler<AuthLog, AuthLogViewModel>(this.Container)
                .Handle(new EntityListQuery<AuthLog, AuthLogViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<AuthLog, AuthLogViewModel>>(),
                    Specification = new AuthLogByPlayerNameSpec(this.Player.Name)
                }).OrderByDescending(x => x.Id).Limit(10);

            return View(vm);
        }

        [Authorize]
        public ActionResult RenderJobs()
        {
            var jobs = new EntityListQueryHandler<Job, JobViewModel>(this.Container)
                .Handle(new EntityListQuery<Job, JobViewModel>()
                {
                    //Specification = new JobByUuidSpec(HashManager.UuiBytes("OfflinePlayer:" + this.Player.Name)),
                    Projector = this.Container.Resolve<IProjector<Job, JobViewModel>>()
                }).Where(x => x.Uuid.IsEquals(HashManager.UuiBytes("OfflinePlayer:" + this.Player.Name)));

            return PartialView("_JobsPartial", jobs);
        }

        #endregion

        #region Private Methods

        private void CheckUserName(string userName)
        {
            if (userName == "") throw new Exception(Resources.ErrorUserNameEmpty);

            if (Regex.IsMatch(userName, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
            {
                throw new Exception(Resources.ErrorUserNameAsEmail);
            }

            var players = new EntityListQueryHandler<Player, PlayerProfileViewModel>(this.Container)
                .Handle(new EntityListQuery<Player, PlayerProfileViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<Player, PlayerProfileViewModel>>(),
                    Specification = new PlayerByLoginSpec(userName)
                });
            
            if(players.Any()) throw new Exception(Resources.ErrorUserNameExists);
        }

        private List<string> CheckPlayerSkin(HttpPostedFileBase skinFile, out byte[] bytes)
        {
            var errors = new List<string>();
            bytes = ImageManager.GetImageBytes(skinFile);

            if (skinFile == null)
            {
                errors.Add("Файл скина не выбран");

                return errors;
            }

            var fileType = skinFile.ContentType;
            var allowedTypes = new List<string>() {"image/png", "image/jpg" };

            if (!allowedTypes.Contains(fileType))
            {
                errors.Add("Скин может быть в формате: .png или .jpg");

                return errors;
            }
            
            var image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(bytes));

            if (image.Width <= 64 && (image.Width / image.Height != 2 && image.Width / image.Height != 1))
            {
                errors.Add("Скины должны быть формата 1:1 или 2:1 (например, 64x64 или 64x32)");
            }

            if (image.Width > 64 && image.Width/image.Height != 2)
            {
                errors.Add("HD скины должны быть формата 2:1 (например, 1024x512)");
            }

            if (image.Width > 1024)
            {
                errors.Add("Максимальный размер скина - 1024x512");
            }
            
            if (skinFile.ContentLength / 1048576 > 1)
            {
                errors.Add("Максимальный размер файла - 1 МБ");
            }
            
            return errors;
        }

        #endregion
    }
}
