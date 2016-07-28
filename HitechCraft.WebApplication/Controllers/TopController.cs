namespace HitechCraft.WebApplication.Controllers
{
    #region Using Directives

    using System.Web.Mvc;
    using Common.DI;
    using System.Linq;
    using BL.CQRS.Query;
    using Common.Projector;
    using DAL.Domain;
    using Models;
    using DAL.Domain.Extentions;

    #endregion

    public class TopController : BaseController
    {
        public TopController(IContainer container) : base(container)
        {
        }

        //TODO: убрать из топа игроков администрации и дефолтные аккаунты
        //TODO: добавить топы по профессиям и пр.
        
        // GET: Top
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TopGonts()
        {
            var vm = new EntityListQueryHandler<Currency, CurrencyTopViewModel>(this.Container)
                .Handle(new EntityListQuery<Currency, CurrencyTopViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<Currency, CurrencyTopViewModel>>()
                }).OrderByDescending(x => x.Gonts).Limit(10);

            return PartialView("_TopGontsPartial", vm);
        }

        public ActionResult TopRubs()
        {
            var vm = new EntityListQueryHandler<Currency, CurrencyTopViewModel>(this.Container)
                .Handle(new EntityListQuery<Currency, CurrencyTopViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<Currency, CurrencyTopViewModel>>()
                }).OrderByDescending(x => x.Rubles).Limit(10);

            return PartialView("_TopRubsPartial", vm);
        }
    }
}