using HitechCraft.Core.DI;
using HitechCraft.Core.Entity;
using HitechCraft.Core.Entity.Extentions;
using HitechCraft.Core.Helper;
using HitechCraft.Core.Models.Enum;
using HitechCraft.Projector.Impl;

namespace HitechCraft.WebApplication.Controllers
{
    #region Using Directives

    using PagedList;
    using BL.CQRS.Query;
    using Models;
    using System.Web.Mvc;
    using System;
    using BL.CQRS.Command;
    using Manager;
    using System.Linq;
    using System.Collections.Generic;
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
            var bytes = HashHelper.GetBase64Bytes(base64);

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
                var image = HashHelper.GetBase64Hash(Manager.FileManager.DownloadFile(url));

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