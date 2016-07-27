using HitechCraft.BL.CQRS.Command;

namespace HitechCraft.WebApplication.Controllers
{
    using System.Web.Mvc;
    using BL.CQRS.Query;
    using Common.DI;
    using Common.Projector;
    using DAL.Domain;
    using Models;

    public class ShopController : BaseController
    {
        public ShopController(IContainer container) : base(container)
        {
        }

        // GET: Shop
        public ActionResult Index()
        {
            var vm = new EntityListQueryHandler<ShopItem, ShopItemViewModel>(this.Container)
                .Handle(new EntityListQuery<ShopItem, ShopItemViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<ShopItem, ShopItemViewModel>>()
                });

            return View(vm);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult CreateItem()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateItem(ShopItemEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                this.CommandExecutor.Execute(this.Project<ShopItemEditViewModel, ShopItemCreateCommand>(vm));

                return RedirectToAction("Index");
            }

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult CreateItemCategory()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateItemCategory(ShopItemCategoryEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                this.CommandExecutor.Execute(this.Project<ShopItemCategoryEditViewModel, ShopItemCategoryCreateCommand>(vm));

                return RedirectToAction("Index");
            }


            return View();
        }
    }
}