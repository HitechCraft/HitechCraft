using WebApplication.Core;

namespace WebApplication.Controllers
{
    #region Using Directives

    using Domain;
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using AutoMapper;
    using Models;
    using System.Collections.Generic;
    using System.Data.Entity;

    #endregion

    public class ServerController : BaseController
    {
        public IEnumerable<Server> Servers { get { return this.context.Servers.Include("ServerModifications.Modification"); } }

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
            var vm = Mapper.Map<IEnumerable<Server>, IEnumerable<ServerViewModel>>(this.Servers);

            return View(vm);
        }
        
        public ActionResult Details(int id)
        {
            try
            {
                var server = this.Servers.First(s => s.Id == id);

                var vm = Mapper.Map<Server, ServerViewModel>(server);

                return View(vm);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;

                return View();
            }
        }

        [HttpGet]
        public ActionResult MonitoringList()
        {
            var vm = Mapper.Map<IEnumerable<Server>, IEnumerable<ServerViewModel>>(this.Servers);

            return View("_ServerMonitoring", vm);
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
                
                var uploadImage = Request.Files["uploadServerImage"];
                
                server.Image = ImageManager.GetImageBytes(uploadImage);

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
                var server = this.Servers.First(s => s.Id == id);

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

                var uploadImage = Request.Files["uploadServerImage"];
                
                if (uploadImage.ContentLength > 0) server.Image = ImageManager.GetImageBytes(uploadImage);

                this.context.Entry(server).State = EntityState.Modified;
                this.context.SaveChanges();

                return RedirectToAction("Details", new { id = server.Id });
            }

            return View(vm);
        }
        
        public ActionResult Delete(int? id)
        {
            try
            {
                var server = this.Servers.First(s => s.Id == id);

                var vm = Mapper.Map<Server, ServerEditViewModel>(server);

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
        public ActionResult DeleteConfirmed(int id)
        {
            var server = this.Servers.First(s => s.Id == id);

            this.context.Servers.Remove(server);
            this.context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
