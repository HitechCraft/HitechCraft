using HitechCraft.BL.CQRS.Query;
using HitechCraft.Common.Projector;
using HitechCraft.DAL.Domain;
using HitechCraft.WebApplication.Models;

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
            var rules = new EntityListQueryHandler<RulePoint, RulePointViewModel>(this.Container)
                .Handle(new EntityListQuery<RulePoint, RulePointViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<RulePoint, RulePointViewModel>>()
                });

            return View(rules);
        }
    }
}