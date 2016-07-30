namespace HitechCraft.WebApplication.Controllers
{
    using System.Web.Mvc;
    using Common.DI;

    public class RuleController : BaseController
    {
        public RuleController(IContainer container) : base(container)
        {
        }

        // GET: Rule
        public ActionResult Index()
        {
            return View();
        }
    }
}