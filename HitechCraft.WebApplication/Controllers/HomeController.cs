using HitechCraft.BL.CQRS.Command;
using HitechCraft.Core.DI;

namespace HitechCraft.WebApplication.Controllers
{
    using HitechCraft.BL.CQRS.Query;
    using HitechCraft.Core.Entity;
    using HitechCraft.Core.Projector;
    using HitechCraft.Core.Repository.Specification.News;
    using HitechCraft.WebApplication.Models;
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        public HomeController(IContainer container) : base(container)
        {
        }
        
        public ActionResult Index(string refer="")
        {
            Session["ReferalId"] = refer;

            ViewBag.NewMessages = this.NewMessagesCount;
            
            return View();
        }

        public ActionResult NewIndex()
        {
            return View();
        }

        public ActionResult GameStart()
        {
            return View();
        }
        
        public ActionResult Rules()
        {
            return RedirectToAction("Index", "Rule");
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

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}