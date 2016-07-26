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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GameStart()
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