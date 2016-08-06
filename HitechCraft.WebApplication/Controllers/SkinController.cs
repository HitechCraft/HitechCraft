namespace HitechCraft.WebApplication.Controllers
{
    using System.Collections.Generic;
    using BL.CQRS.Query;
    using Common.DI;
    using Common.Projector;
    using DAL.Domain;
    using Models;
    using System.Web.Mvc;

    public class SkinController : BaseController
    {
        public SkinController(IContainer container) : base(container)
        {
        }

        #region Actions

        [Authorize]
        public ActionResult Catalog()
        {
            var vm = GetSkins();

            return View(vm);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        #endregion

        #region Private Methods

        private IEnumerable<SkinViewModel> GetSkins()
        {
            return new EntityListQueryHandler<Skin, SkinViewModel>(Container)
                .Handle(new EntityListQuery<Skin, SkinViewModel>()
                {
                    Projector = Container.Resolve<IProjector<Skin, SkinViewModel>>()
                });
        }

        #endregion
    }
}