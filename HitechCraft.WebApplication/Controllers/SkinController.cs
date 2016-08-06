namespace HitechCraft.WebApplication.Controllers
{
    using System.Collections.Generic;
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
        public SkinController(IContainer container) : base(container)
        {
        }

        #region Actions

        [Authorize]
        public ActionResult Catalog()
        {
            var vm = GetSkins();

            return View(vm);
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

        #endregion

        #region Private Methods

        private IEnumerable<SkinViewModel> GetSkins()
        {
            return new EntityListQueryHandler<Skin, SkinViewModel>(Container)
                .Handle(new EntityListQuery<Skin, SkinViewModel>()
                {
                    Projector = Container.Resolve<IProjector<Skin, SkinViewModel>>()
                });
        }

        #endregion
    }
}