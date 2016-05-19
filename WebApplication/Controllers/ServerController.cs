namespace WebApplication.Controllers
{
    using Areas.Launcher.Models.Json;
    using Domain;
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using AutoMapper;
    using Models;
    using System.Collections.Generic;
    using System.Data.Entity;
    public class ServerController : BaseController
    {
        public ServerController()
        {
            Mapper.CreateMap<Server, ServerViewModel>()
               .ForMember(dst => dst.Modifications, exp => exp.MapFrom(
                   src => src.ServerModifications != null ? src.ServerModifications
                       .Select(sm => new ServerModificationViewModel()
                       {
                           Id = sm.Modification.Id,
                           Name = sm.Modification.Name
                       }).ToList() : new List<ServerModificationViewModel>()))
                .ForMember(dst => dst.ServerData, exp => exp.MapFrom(
                   src => src.GetServerData()));
            
            Mapper.CreateMap<Server, ServerEditViewModel>();
            Mapper.CreateMap<ServerEditViewModel, Server>();
        }

        public ActionResult Index()
        {
            IEnumerable<Server> servers = this.context.Servers.Include("ServerModifications.Modification").ToList();
            
            var vm = Mapper.Map<IEnumerable<Server>, IEnumerable<ServerViewModel>>(servers);

            return View(vm.ToList());
        }
        
        public ActionResult Details(int id)
        {
            try
            {
                var server = this.context.Servers.Include("ServerModifications.Modification").First(s => s.Id == id);

                var vm = Mapper.Map<Server, ServerViewModel>(server);

                return View(vm);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;

                return View();
            }
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(ServerEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var server = Mapper.Map<ServerEditViewModel, Server>(vm);

                this.context.Servers.Add(server);
                this.context.SaveChanges();

                return RedirectToAction("Details", new { id = server.Id });
            }

            return View(vm);
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var server = this.context.Servers.First(s => s.Id == id);

                var vm = Mapper.Map<Server, ServerEditViewModel>(server);

                return View(vm);
            }
            catch (Exception e)
            {
                ViewBag.Error = "Сервера не существует";

                return View();
            }
        }

        [HttpPost]
        public ActionResult Edit(ServerEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var server = Mapper.Map<ServerEditViewModel, Server>(vm);

                this.context.Entry(server).State = EntityState.Modified;
                this.context.SaveChanges();

                return RedirectToAction("Details", new { id = server.Id });
            }

            return View(vm);
        }
    }
}
