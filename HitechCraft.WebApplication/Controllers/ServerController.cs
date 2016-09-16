using HitechCraft.Core.DI;
using HitechCraft.Core.Entity;
using HitechCraft.Projector.Impl;

namespace HitechCraft.WebApplication.Controllers
{
    #region Using Directives

    using System.Web.Mvc;
    using BL.CQRS.Query;
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
    }
}