namespace HitechCraft.WebApplication.Controllers
{
    using System.Web.Mvc;
    using Common.DI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BL.CQRS.Command;
    using BL.CQRS.Query;
    using Common.Projector;
    using Common.Repository.Specification;
    using DAL.Domain;
    using DAL.Repository.Specification;
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

        [Authorize]
        public JsonResult CreateRulePoint(string name)
        {
            try
            {
                var rulePoint = this.GetRules(new RulePointByNameSpec(name)).First();

                return Json(new { status = "NO", message = "Пункт правил уже существует" });
            }
            catch (Exception)
            {
                this.CommandExecutor.Execute(new RulePointCreateCommand()
                {
                    Name = name
                });

                return Json(new { status = "OK", message = "Пункт правил добавлен" });
            }
        }

        [Authorize]
        public JsonResult CreateRule(int pointId, string text)
        {
            try
            {
                this.CommandExecutor.Execute(new RuleCreateCommand()
                {
                    PointId = pointId,
                    Text = text
                });

                return Json(new { status = "OK", message = "Правило успешно добавлено" });
            }
            catch (Exception e)
            {
                return Json(new { status = "NO", message = "Ошибка добавления правила. " + e.Message });
            }
        }

        [Authorize]
        public JsonResult DeleteRulePoint(int id)
        {
            try
            {
                this.CommandExecutor.Execute(new RulePointRemoveCommand()
                {
                    Id = id
                });

                return Json(new { status = "OK", message = "Пункт удален" });
            }
            catch (Exception e)
            {
                return Json(new { status = "NO", message = "Ошибка удаления. " + e.Message });
            }
        }

        [Authorize]
        public JsonResult DeleteRule(int id)
        {
            try
            {
                this.CommandExecutor.Execute(new RuleRemoveCommand()
                {
                    Id = id
                });

                return Json(new { status = "OK", message = "Правило удалено" });
            }
            catch (Exception e)
            {
                return Json(new { status = "NO", message = "Ошибка удаления. " + e.Message });
            }
        }

        [HttpPost]
        public JsonResult EditRule(int id, string text)
        {
            try
            {
                this.CommandExecutor.Execute(new RuleUpdateCommand()
                {
                    Id = id,
                    Text = text
                });

                return Json(new {status = "OK", message = "Правило обновлено!"});
            }
            catch (Exception e)
            {
                return Json(new { status = "NO", message = "Ошибка обновления правила: " + e.Message });
            }
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