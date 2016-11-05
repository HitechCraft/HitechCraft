namespace HitechCraft.WebAdmin.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using BL.CQRS.Command;
    using BL.CQRS.Query;
    using Core.DI;
    using Core.Entity;
    using Projector.Impl;
    using Manager;
    using Models;
    using PagedList;

    public class ServerController : BaseController
    {
        public int ServersOnPage => 10;

        public ServerController(IContainer container) : base(container)
        {
        }

        // GET: Server
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ServerPartialList(int? page, string serverTitleFilter = "")
        {
            int currentPage = page ?? 1;

            var servers = new EntityListQueryHandler<Server, ServerViewModel>(Container)
                .Handle(new EntityListQuery<Server, ServerViewModel>()
                {
                    Projector = Container.Resolve<IProjector<Server, ServerViewModel>>()
                });

            if (!String.IsNullOrEmpty(serverTitleFilter))
                servers =
                    servers.Where(x => x.Name.Contains(serverTitleFilter) || x.Description.Contains(serverTitleFilter)).ToList();

            return PartialView("_ServerPartialList", servers.ToPagedList(currentPage, ServersOnPage));
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
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
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Edit(ServerEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var uploadImage = Request.Files["uploadServerImage"];

                if (uploadImage.ContentLength > 0) vm.Image = ImageManager.GetImageBytes(uploadImage);

                this.CommandExecutor.Execute(this.Project<ServerEditViewModel, ServerUpdateCommand>(vm));
                
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        public ActionResult Details(int id)
        {
            try
            {
                var vm = new EntityQueryHandler<Server, ServerViewModel>(this.Container)
                    .Handle(new EntityQuery<Server, ServerViewModel>()
                    {
                        Id = id,
                        Projector = this.Container.Resolve<IProjector<Server, ServerViewModel>>()
                    });

                return View(vm);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;

                return View();
            }
        }
        
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null || id.Value == 0) throw new Exception("Id не найден");

                this.CommandExecutor.Execute(new ServerRemoveCommand()
                {
                    Id = id.Value
                });

                return Json(new {status = "OK", message = "Сервер успешно удален"});
            }
            catch (Exception e)
            {
                return Json(new { status = "NO", message = "Ошибка удаления сервера: " + e.Message });
            }
        }
    }
}