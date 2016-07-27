using System;

namespace HitechCraft.WebApplication.Controllers
{
    #region Using Directives

    using System.Web.Mvc;
    using BL.CQRS.Query;
    using Common.DI;
    using Common.Projector;
    using DAL.Domain;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using BL.CQRS.Command;
    using Manager;
    using PagedList;

    #endregion

    public class ShopController : BaseController
    {
        public int ItemsOnPage => 12;

        public ShopController(IContainer container) : base(container)
        {
        }

        #region Shop Actions

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

        [HttpGet]
        public ActionResult ItemPartialList(int? page, int? modId, int? categoryId, string filterText = null)
        {
            int currentPage = page ?? 1;

            var itemList = this.GetItemList(currentPage);

            if (modId != null)
            {
                itemList = itemList.Where(x => x.ModificationId == modId).ToPagedList(currentPage, this.ItemsOnPage);
            }

            if (categoryId != null)
            {
                itemList = itemList.Where(x => x.CategoryId == categoryId).ToPagedList(currentPage, this.ItemsOnPage);
            }

            if (!string.IsNullOrEmpty(filterText))
            {
                itemList = itemList.Where(x => x.Name.Contains(filterText) || x.Description.Contains(filterText)).ToPagedList(currentPage, this.ItemsOnPage);
            }

            return PartialView("_ShopItemListPartial", itemList);
        }

        [HttpGet]
        public ActionResult ItemPartial(string gameId)
        {
            return PartialView("_ShopItemPartial", this.GetItem(gameId));
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

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult DeleteItem(string gameId)
        {
            try
            {
                if (gameId != String.Empty)
                {
                    this.CommandExecutor.Execute(new ShopItemRemoveCommand()
                    {
                        GameId = gameId
                    });

                    return Json(new { status = "OK", message = "Предмет успешно удален" });
                }

                return Json(new { status = "NO", message = "Ошибка удаления предмета" });
            }
            catch (Exception)
            {
                return Json(new { status = "NO", message = "Ошибка удаления предмета" });
            }
        }

        #endregion

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

        private IPagedList<ShopItemViewModel> GetItemList(int page)
        {
            var vm = new EntityListQueryHandler<ShopItem, ShopItemViewModel>(this.Container)
                .Handle(new EntityListQuery<ShopItem, ShopItemViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<ShopItem, ShopItemViewModel>>()
                }).OrderBy(x => x.GameId);

            return vm.ToPagedList(page, this.ItemsOnPage);
        }

        private ShopItemViewModel GetItem(string gameId)
        {
            return new EntityQueryHandler<ShopItem, ShopItemViewModel>(this.Container)
                .Handle(new EntityQuery<ShopItem, ShopItemViewModel>()
                {
                    Id = gameId,
                    Projector = this.Container.Resolve<IProjector<ShopItem, ShopItemViewModel>>()
                });
        }

        #endregion
    }
}