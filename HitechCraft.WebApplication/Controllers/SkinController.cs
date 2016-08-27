namespace HitechCraft.WebApplication.Controllers
{
    #region Using Directives

    using PagedList;
    using BL.CQRS.Query;
    using Common.DI;
    using Common.Projector;
    using DAL.Domain;
    using Models;
    using System.Web.Mvc;
    using System;
    using BL.CQRS.Command;
    using Manager;
    using System.Linq;
    using DAL.Domain.Extentions;
    using System.Collections.Generic;
    using Common.Core;
    using Common.Models.Enum;
    using Properties;

    #endregion

    public class SkinController : BaseController
    {
        public int SkinsOnPage => 8;

        public SkinController(IContainer container) : base(container)
        {
        }

        #region Actions

        public ActionResult SkinPartialList(int? page)
        {
            return PartialView("_SkinPartialList", this.GetSkins(page));
        }

        [Authorize]
        public ActionResult Catalog()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.Gender = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = Resources.GenderMale,
                    Value = ((int)Gender.Male).ToString(),
                    Selected = true
                },

                new SelectListItem()
                {
                    Text = Resources.GenderFemale,
                    Value = ((int)Gender.Female).ToString()
                }
            };

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(SkinEditViewModel vm)
        {
            var uploadFile = Request.Files["uploadSkinImage"];

            if(uploadFile == null) ModelState.AddModelError(String.Empty, "Выберите изображение");

            if (ModelState.IsValid)
            {
                this.CommandExecutor.Execute(new SkinCreateCommand()
                {
                    Name = vm.Name,
                    Image = ImageManager.GetImageBytes(uploadFile),
                    Gender = vm.Gender
                });

                return RedirectToAction("Create");
            }

            ViewBag.Gender = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = Resources.GenderMale,
                    Value = ((int)Gender.Male).ToString(),
                    Selected = true
                },

                new SelectListItem()
                {
                    Text = Resources.GenderFemale,
                    Value = ((int)Gender.Female).ToString()
                }
            };

            return View();
        }

        [HttpPost]
        public ActionResult CheckExistingSkin()
        {
            var uploadedFile = Request.Files["uploadSkinImage"];

            var bytes = ImageManager.GetImageBytes(uploadedFile);

            var skins = new EntityListQueryHandler<Skin, SkinViewModel>(this.Container)
                .Handle(new EntityListQuery<Skin, SkinViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<Skin, SkinViewModel>>()
                }).Where(x => x.Image.IsEquals(bytes));

            if (skins.Any())
            {
                return PartialView("_SkinDuplicatedPartial", skins.First());
            }

            return this.Content("OK");
        }

        [HttpPost]
        public ActionResult CheckExistingSkinByBase64(string base64)
        {
            var bytes = HashManager.GetBase64Bytes(base64);

            var skins = new EntityListQueryHandler<Skin, SkinViewModel>(this.Container)
                .Handle(new EntityListQuery<Skin, SkinViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<Skin, SkinViewModel>>()
                }).Where(x => x.Image.IsEquals(bytes));

            if (skins.Any())
            {
                return PartialView("_SkinDuplicatedPartial", skins.First());
            }

            return this.Content("OK");
        }

        [HttpPost]
        public JsonResult SetPlayerSkin(int? skinId)
        {
            try
            {
                if(skinId == null) throw new Exception("Не передан ID скина!");

                this.CommandExecutor.Execute(new SkinInstallCommand()
                {
                    PlayerName = this.Player.Name,
                    SkinId = skinId.Value
                });
            }
            catch (Exception e)
            {
                return Json(new {status = "NO", message = "Ошибка установки скина: " + e.Message});
            }

            return Json(new { status = "OK", message = "Скин успешно установлен!" });
        }


        [HttpPost]
        public JsonResult DownloadSkin(string url)
        {
            try
            {
                var image = HashManager.GetBase64Hash(Manager.FileManager.DownloadFile(url));

                return Json(new { status = "OK", message = @"data:image/jpeg;base64," + image, img = image });
            }
            catch (Exception ex)
            {
                return Json(new { status = "NO", message = "Error: " + ex.Message });
            }
        }

        #endregion

        #region Private Methods

        private IPagedList<SkinViewModel> GetSkins(int? page)
        {
            int currentPage = page ?? 1;

            var vm = new EntityListQueryHandler<Skin, SkinViewModel>(Container)
                .Handle(new EntityListQuery<Skin, SkinViewModel>()
                {
                    Projector = Container.Resolve<IProjector<Skin, SkinViewModel>>()
                });

            return vm.ToPagedList(currentPage, this.SkinsOnPage);
        }
        
        #endregion
    }
}