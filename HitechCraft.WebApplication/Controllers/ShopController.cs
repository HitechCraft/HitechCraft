using HitechCraft.Core;
using HitechCraft.Core.DI;
using HitechCraft.Core.Entity;
using HitechCraft.Core.Repository.Specification.PlayerItem;
using HitechCraft.Projector.Impl;

namespace HitechCraft.WebApplication.Controllers
{
    #region Using Directives

    using System;
    using System.Web.Mvc;
    using BL.CQRS.Query;
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

        //TODO: общую скидку засчет каких нибудь достижений
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

            if (Const.ShopEnable)
            {
                return View(vm);
            }
            else
            {
                return View("ServiceUnavailable");
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult ItemPartialList(int? page, int? modId, int? categoryId, int? itemOnPage, string filterText = null)
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
                itemList = itemList.Where(x => x.Name.Contains(filterText));
            }
            
            ViewBag.ItemsOnPage = itemOnPage ?? this.ItemsOnPage;
            ViewBag.Page = currentPage;

            return PartialView("_ShopItemListPartial", itemList.ToPagedList(currentPage, itemOnPage ?? this.ItemsOnPage));
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
        
        [Authorize]
        public ActionResult BuyItem(string gameId)
        {
            ShopItemViewModel vm;

            try
            {
                if (String.IsNullOrEmpty(gameId)) throw new Exception();

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
                if(count < 1) throw new Exception("Нельзя приобрести " + count + " ресурсa(ов)!");

                if (count > 576) throw new Exception("Нельзя приобретать больше 576 (9 стаков) за раз!");

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