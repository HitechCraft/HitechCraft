namespace WebApplication.Controllers
{
    using Areas.Launcher.Models.Json;
    using Core;
    using Domain;
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using AutoMapper;
    using Models;
    using System.Collections.Generic;

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

        public JsonResult ReturnServerData(string ipAddress, int? port)
        {
            try
            {
                var server = this.context.Servers.First(s => s.IpAddress == ipAddress && s.Port == (port != null ? port : 25565).Value);

                return Json(server.GetServerData(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonServerData()
                {
                    Status = JsonServerStatus.Error,
                    Message = "Указанный сервер не найден"
                },
                JsonRequestBehavior.AllowGet);
            }
        }
    }
}
