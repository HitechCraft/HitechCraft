using System.Collections.Generic;
using System.Linq;
using System.Web;
using HitechCraft.DAL.Domain.Extentions;
using HitechCraft.DAL.Repository.Specification;

namespace HitechCraft.WebApplication.Controllers
{
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
                    Image = ImageManager.GetImageBytes(uploadFile)
                });

                return RedirectToAction("Create");
            }

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