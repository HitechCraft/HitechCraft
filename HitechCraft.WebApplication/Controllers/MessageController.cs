namespace HitechCraft.WebApplication.Controllers
{
    using System.Web.Mvc;

    [Authorize]
    public class MessageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}