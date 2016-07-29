namespace HitechCraft.WebApplication.Controllers
{
    #region Using Directives

    using System;
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
    using DAL.Repository.Specification;

    #endregion

    public class ShopController : BaseController
    {
        public int ItemsOnPage => 8;

        public ShopController(IContainer container) : base(container)
        {
        }

        #region Shop Actions

        // GET: Shop
        [Authorize]
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
        [Authorize]
        public ActionResult ItemPartialList(int? page, int? modId, int? categoryId, string filterText = null)
        {
            int currentPage = page ?? 1;
            
            var itemList = (IEnumerable<ShopItemViewModel>)new EntityListQueryHandler<ShopItem, ShopItemViewModel>(this.Container)
                .Handle(new EntityListQuery<ShopItem, ShopItemViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<ShopItem, ShopItemViewModel>>()
                }).OrderBy(x => x.GameId);

            if (modId != null && modId != 0)
            {
                itemList = itemList.Where(x => x.ModificationId == modId);
            }

            if (categoryId != null && categoryId != 0)
            {
                itemList = itemList.Where(x => x.CategoryId == categoryId);
            }

            if (!string.IsNullOrEmpty(filterText))
            {
                itemList = itemList.Where(x => x.Name.Contains(filterText) || x.Description.Contains(filterText));
            }
            
            ViewBag.ItemsOnPage = this.ItemsOnPage;
            ViewBag.Page = currentPage;

            return PartialView("_ShopItemListPartial", itemList.ToPagedList(currentPage, this.ItemsOnPage));
        }

        [HttpGet]
        [Authorize]
        public ActionResult ItemPartial(string gameId)
        {
            return PartialView("_ShopItemPartial", this.GetItem(gameId));
        }

        [Authorize]
        public ActionResult Cart()
        {
            var items = new EntityListQueryHandler<PlayerItem, PlayerItemViewModel>(this.Container)
                .Handle(new EntityListQuery<PlayerItem, PlayerItemViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<PlayerItem, PlayerItemViewModel>>(),
                    Specification = new PlayerItemByPlayerNameSpec(this.Player.Name)
                });

            return View(items);
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
            try
            {
                if (ModelState.IsValid)
                {
                    var uploadImage = Request.Files["uploadShopItemImage"];
                    vm.Image = ImageManager.GetImageBytes(uploadImage);

                    this.CommandExecutor.Execute(this.Project<ShopItemEditViewModel, ShopItemCreateCommand>(vm));

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
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

            return View(vm);
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

        [Authorize(Roles = "Administrator")]
        public ActionResult EditItem(string gameId)
        {
            ShopItemEditViewModel vm;

            try
            {
                vm = new EntityQueryHandler<ShopItem, ShopItemEditViewModel>(this.Container)
                   .Handle(new EntityQuery<ShopItem, ShopItemEditViewModel>()
                   {
                       Id = gameId,
                       Projector = this.Container.Resolve<IProjector<ShopItem, ShopItemEditViewModel>>()
                   });
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
            
            ViewBag.Mods = this.GetMods().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = (x.Id == vm.ModificationId)
            });

            ViewBag.Categories = this.GetCategories().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = (x.Id == vm.CategoryId)
            });

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditItem(ShopItemEditViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var uploadImage = Request.Files["uploadShopItemImage"];
                    if(uploadImage != null && uploadImage.ContentLength > 0) vm.Image = ImageManager.GetImageBytes(uploadImage);

                    this.CommandExecutor.Execute(this.Project<ShopItemEditViewModel, ShopItemUpdateCommand>(vm));

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            
            ViewBag.Mods = this.GetMods().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = (x.Id == vm.ModificationId)
            });

            ViewBag.Categories = this.GetCategories().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = (x.Id == vm.CategoryId)
            });

            return View(vm);
        }

        [Authorize]
        public ActionResult BuyItem(string gameId)
        {
            ShopItemViewModel vm;

            try
            {
                vm = new EntityQueryHandler<ShopItem, ShopItemViewModel>(this.Container)
                   .Handle(new EntityQuery<ShopItem, ShopItemViewModel>()
                   {
                       Id = gameId,
                       Projector = this.Container.Resolve<IProjector<ShopItem, ShopItemViewModel>>()
                   });
            }
            catch (Exception)
            {
                return HttpNotFound();
            }

            ViewBag.Balance = this.Currency.Gonts;

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public JsonResult BuyItem(string gameId, int count)
        {
            try
            {
                if(count == 0) throw new Exception("Нельзя приобрести 0 кол-во ресурсов!");

                this.CommandExecutor.Execute(new ShopItemBuyCommand()
                {
                    Count = count,
                    GameId = gameId,
                    PlayerName = this.Player.Name
                });

                return Json(new { status = "OK", message = "Товар успешно приобретен" });
            }
            catch (Exception e)
            {
                return Json(new {status = "NO", message = "Ошибка покупки предмета. " + e.Message});
            }
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