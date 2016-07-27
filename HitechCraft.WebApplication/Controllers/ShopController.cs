using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using HitechCraft.BL.CQRS.Command;
using HitechCraft.WebApplication.Manager;

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
            
            ViewBag.Mods = this.GetMods();

            ViewBag.Categories = this.GetCategories();

            return View(vm);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult CreateItem()
        {
            ViewBag.Mods = this.GetMods().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            ViewBag.Categories = this.GetCategories().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateItem(ShopItemEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var uploadImage = Request.Files["uploadShopItemImage"];
                vm.Image = ImageManager.GetImageBytes(uploadImage);

                this.CommandExecutor.Execute(this.Project<ShopItemEditViewModel, ShopItemCreateCommand>(vm));

                return RedirectToAction("Index");
            }

            ViewBag.Mods = this.GetMods().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            ViewBag.Categories = this.GetCategories().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult CreateItemCategory()
        {
            var categories = new EntityListQueryHandler<ShopItemCategory, ShopItemCategoryViewModel>(this.Container)
                .Handle(new EntityListQuery<ShopItemCategory, ShopItemCategoryViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<ShopItemCategory, ShopItemCategoryViewModel>>()
                });

            ViewBag.Categories = categories;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateItemCategory(ShopItemCategoryEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                this.CommandExecutor.Execute(this.Project<ShopItemCategoryEditViewModel, ShopItemCategoryCreateCommand>(vm));

                return RedirectToAction("CreateItemCategory");
            }


            return View();
        }

        #region Private Methods

        private ICollection<ModificationViewModel> GetMods()
        {
            return new EntityListQueryHandler<Modification, ModificationViewModel>(this.Container)
                .Handle(new EntityListQuery<Modification, ModificationViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<Modification, ModificationViewModel>>()
                });
        }

        private ICollection<ShopItemCategoryViewModel> GetCategories()
        {
            return new EntityListQueryHandler<ShopItemCategory, ShopItemCategoryViewModel>(this.Container)
                .Handle(new EntityListQuery<ShopItemCategory, ShopItemCategoryViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<ShopItemCategory, ShopItemCategoryViewModel>>()
                });
        }


        private ICollection<ShopItemViewModel> GetItems()
        {
            return new EntityListQueryHandler<ShopItem, ShopItemViewModel>(this.Container)
                .Handle(new EntityListQuery<ShopItem, ShopItemViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<ShopItem, ShopItemViewModel>>()
                });
        }

        #endregion
    }
}