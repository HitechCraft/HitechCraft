using System;
using System.Linq;
using System.Web.Mvc;
using HitechCraft.BL.CQRS.Command;
using HitechCraft.BL.CQRS.Query;
using HitechCraft.Core.DI;
using HitechCraft.Core.Entity;
using HitechCraft.Core.Projector;

using HitechCraft.WebAdmin.Manager;
using HitechCraft.WebAdmin.Models;
using PagedList;

namespace HitechCraft.WebAdmin.Controllers
{
    public class ModificationController : BaseController
    {
        public int ModsOnPage => 10;

        public ModificationController(IContainer container) : base(container)
        {
        }

        // GET: Modification
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ModPartialList(int? page, string modTitleFilter = "")
        {
            int currentPage = page ?? 1;

            var mods = new EntityListQueryHandler<Modification, ModificationViewModel>(this.Container)
                .Handle(new EntityListQuery<Modification, ModificationViewModel>()
                {
                    Projector = Container.Resolve<IProjector<Modification, ModificationViewModel>>()
                });

            if (!String.IsNullOrEmpty(modTitleFilter))
                mods =
                    mods.Where(x => x.Name.Contains(modTitleFilter) || x.Description.Contains(modTitleFilter)).ToList();

            return PartialView("_ModPartialList", mods.ToPagedList(currentPage, this.ModsOnPage));
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ModificationEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var uploadImage = Request.Files["uploadModImage"];

                vm.Image = ImageManager.GetImageBytes(uploadImage);

                this.CommandExecutor.Execute(this.Project<ModificationEditViewModel, ModificationCreateCommand>(vm));
                
                return RedirectToAction("Index");
            }

            return View(vm);
        }
        
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == 0 || id == null) throw new Exception("Id не найден");

                var vm = new EntityQueryHandler<Modification, ModificationEditViewModel>(this.Container)
                    .Handle(new EntityQuery<Modification, ModificationEditViewModel>()
                    {
                        Id = id,
                        Projector = this.Container.Resolve<IProjector<Modification, ModificationEditViewModel>>()
                    });

                return View(vm);
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Edit(ModificationEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var uploadImage = Request.Files["uploadModImage"];

                if (uploadImage != null && uploadImage.ContentLength > 0) vm.Image = ImageManager.GetImageBytes(uploadImage);

                this.CommandExecutor.Execute(this.Project<ModificationEditViewModel, ModificationUpdateCommand>(vm));

                return RedirectToAction("Details", new { id = vm.Id });
            }

            return View(vm);
        }

        public ActionResult Details(int? id)
        {
            try
            {
                if (id == 0 || id == null) throw new Exception();

                var vm = new EntityQueryHandler<Modification, ModificationViewModel>(this.Container)
                    .Handle(new EntityQuery<Modification, ModificationViewModel>()
                    {
                        Id = id,
                        Projector = this.Container.Resolve<IProjector<Modification, ModificationViewModel>>()
                    });

                return View(vm);
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }
        
        [HttpPost]
        public JsonResult Delete(int? id)
        {
            try
            {
                if (id == 0 || id == null) throw new Exception("Id не найден");

                this.CommandExecutor.Execute(new ModificationRemoveCommand()
                {
                    Id = id.Value
                });

                return Json(new { status = "OK", message = "Модификация удалена" });
            }
            catch (Exception e)
            {
                return Json(new { status = "NO", message = "Ошибка удаления модификации: " + e.Message });
            }
        }
    }
}