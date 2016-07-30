using System.IO;
using HitechCraft.Common.Core;

namespace HitechCraft.WebApplication.Controllers
{
    using System.Web.Mvc;
    using Common.DI;

    public class HomeController : BaseController
    {
        public HomeController(IContainer container) : base(container)
        {
        }

        //TODO: custom 404 pls

        public ActionResult Index(int? refer)
        {
            //TODO: придумать как запоминать id реферала для текущей сессии!!!

            return View();
        }

        public ActionResult GameStart()
        {
            return View();
        }

        //TODO: возможно перенести правила в базу, т.к. данные будут динамически изменяться
        public ActionResult Rules()
        {
            return View();
        }

        public ActionResult Vote()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}