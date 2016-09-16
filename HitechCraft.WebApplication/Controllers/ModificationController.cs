using HitechCraft.Core.DI;
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
    }
}