using HitechCraft.Core.DI;
using HitechCraft.Core.Entity;
using HitechCraft.Core.Repository.Specification;
using HitechCraft.Core.Repository.Specification.Rule;
using HitechCraft.Projector.Impl;

namespace HitechCraft.WebApplication.Controllers
{
    using System.Web.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BL.CQRS.Command;
    using BL.CQRS.Query;
    using Models;

    public class RuleController : BaseController
    {
        public RuleController(IContainer container) : base(container)
        {
        }
        
        // GET: Rule
        public ActionResult Index()
        {
            var rules = this.GetRules();

            return View(rules);
        }
        
        #region Private Methods

        private IEnumerable<RulePointViewModel> GetRules(ISpecification<RulePoint> spec = null)
        {
            return new EntityListQueryHandler<RulePoint, RulePointViewModel>(this.Container)
                .Handle(new EntityListQuery<RulePoint, RulePointViewModel>()
                {
                    Specification = spec,
                    Projector = this.Container.Resolve<IProjector<RulePoint, RulePointViewModel>>()
                });
        } 

        #endregion
    }
}