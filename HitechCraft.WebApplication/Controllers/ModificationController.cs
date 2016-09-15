﻿using HitechCraft.Core.DI;
using HitechCraft.Core.Entity;
using HitechCraft.Projector.Impl;

namespace HitechCraft.WebApplication.Controllers
{
    using System.Web.Mvc;
    using BL.CQRS.Query;
    using Models;
    using System;
    using BL.CQRS.Command;
    using Manager;

    public class ModificationController : BaseController
    {
        public ModificationController(IContainer container) : base(container)
        {
        }

        // GET: Modification
        public ActionResult Index()
        {
            var vm = new EntityListQueryHandler<Modification, ModificationViewModel>(this.Container)
                .Handle(new EntityListQuery<Modification, ModificationViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<Modification, ModificationViewModel>>()
                });

            return View(vm);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(ModificationEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var uploadImage = Request.Files["uploadModImage"];

                vm.Image = ImageManager.GetImageBytes(uploadImage);

                this.CommandExecutor.Execute(this.Project<ModificationEditViewModel, ModificationCreateCommand>(vm));

                //TODO: redirect to details by id
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == 0 || id == null) throw new Exception();

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
        [Authorize(Roles = "Administrator")]
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
                if(id == 0 || id == null) throw new Exception();

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

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == 0 || id == null) throw new Exception();

            try
            {
                var vm = new EntityQueryHandler<Modification, ModificationEditViewModel>(this.Container)
                    .Handle(new EntityQuery<Modification, ModificationEditViewModel>()
                    {
                        Id = id,
                        Projector = this.Container.Resolve<IProjector<Modification, ModificationEditViewModel>>()
                    });

                return View(vm);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            this.CommandExecutor.Execute(new ModificationRemoveCommand()
            {
                Id = id
            });

            return RedirectToAction("Index");
        }
    }
}