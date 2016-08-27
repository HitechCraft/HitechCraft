namespace HitechCraft.WebApplication.Controllers
{
    #region Using Directives

    using Common.DI;
    using System.Web.Mvc;
    using BL.CQRS.Query;
    using Common.Projector;
    using DAL.Domain;
    using Models;
    using System;
    using BL.CQRS.Command;
    using Manager;

    #endregion

    public class ServerController : BaseController
    {
        public ServerController(IContainer container) : base(container)
        {
        }

        public ActionResult Index()
        {
            var vm = new EntityListQueryHandler<Server, ServerViewModel>(this.Container)
                .Handle(new EntityListQuery<Server, ServerViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<Server, ServerViewModel>>()
                });

            return View(vm);
        }

        [HttpGet]
        public ActionResult MonitoringList()
        {
            var vm = new EntityListQueryHandler<Server, ServerViewModel>(this.Container)
                .Handle(new EntityListQuery<Server, ServerViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<Server, ServerViewModel>>()
                });

            return View("_ServerMonitoring", vm);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(ServerEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var uploadImage = Request.Files["uploadServerImage"];

                vm.Image = ImageManager.GetImageBytes(uploadImage);

                this.CommandExecutor.Execute(this.Project<ServerEditViewModel, ServerCreateCommand>(vm));

                //TODO: redirect to details by id
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            try
            {
                var vm = new EntityQueryHandler<Server, ServerEditViewModel>(this.Container)
                    .Handle(new EntityQuery<Server, ServerEditViewModel>()
                    {
                        Id = id,
                        Projector = this.Container.Resolve<IProjector<Server, ServerEditViewModel>>()
                    });

                return View(vm);
            }
            catch (Exception e)
            {
                ViewBag.Error = "Сервера не существует";

                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(ServerEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var uploadImage = Request.Files["uploadServerImage"];

                if (uploadImage.ContentLength > 0) vm.Image = ImageManager.GetImageBytes(uploadImage);

                this.CommandExecutor.Execute(this.Project<ServerEditViewModel, ServerUpdateCommand>(vm));

                //TODO: redirect to details by id
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        public ActionResult Details(int id)
        {
            try
            {
                var vm = new EntityQueryHandler<Server, ServerDetailViewModel>(this.Container)
                    .Handle(new EntityQuery<Server, ServerDetailViewModel>()
                    {
                        Id = id,
                        Projector = this.Container.Resolve<IProjector<Server, ServerDetailViewModel>>()
                    });

                return View(vm);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;

                return View();
            }
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            try
            {
                var vm = new EntityQueryHandler<Server, ServerEditViewModel>(this.Container)
                       .Handle(new EntityQuery<Server, ServerEditViewModel>()
                       {
                           Id = id,
                           Projector = this.Container.Resolve<IProjector<Server, ServerEditViewModel>>()
                       });

                return View(vm);
            }
            catch (Exception)
            {
                ViewBag.Error = "Сервера не существует";

                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            this.CommandExecutor.Execute(new ServerRemoveCommand()
            {
                Id = id
            });

            return RedirectToAction("Index");
        }
    }
}