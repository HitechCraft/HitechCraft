using System;
using System.Collections.Generic;
using System.Linq;
using HitechCraft.BL.CQRS.Command;
using HitechCraft.BL.CQRS.Query;
using HitechCraft.Common.DI;
using HitechCraft.Common.Projector;
using HitechCraft.DAL.Domain;
using HitechCraft.WebAdmin.Manager;
using HitechCraft.WebAdmin.Models;
using HitechCraft.WebAdmin.Models.Modification;
using PagedList;

namespace HitechCraft.WebAdmin.Controllers
{
    using System.Web.Mvc;

    public class ShopItemController : BaseController
    {
        public int ItemsOnPage => 15;

        public ShopItemController(IContainer container) : base(container)
        {
        }

        // GET: ShopItem
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ItemsPartialList(int? page, string newsTitleFilter = "")
        {
            int currentPage = page ?? 1;

            var items = new EntityListQueryHandler<ShopItem, ShopItemViewModel>(this.Container)
                .Handle(new EntityListQuery<ShopItem, ShopItemViewModel>()
                {
                    Projector = Container.Resolve<IProjector<ShopItem, ShopItemViewModel>>()
                });

            if (!String.IsNullOrEmpty(newsTitleFilter))
                items =
                    items.Where(x => x.Name.Contains(newsTitleFilter)).ToList();

            return PartialView("_ShopItemListPartial", items.ToPagedList(currentPage, ItemsOnPage));
        }
        
        public ActionResult CreateItem()
        {
            ViewBag.Mods = this.GetMods().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = x.Name == "Vanilla"
            });

            ViewBag.Categories = this.GetCategories().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return View();
        }

        [HttpPost]
        public ActionResult CreateItem(ShopItemEditViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var uploadImage = Request.Files["uploadShopItemImage"];
                    vm.Image = ImageManager.GetImageBytes(uploadImage);

                    this.CommandExecutor.Execute(this.Project<ShopItemEditViewModel, ShopItemCreateCommand>(vm));

                    return RedirectToAction("CreateItem");
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
        public ActionResult CreateItemCategory(ShopItemCategoryEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                this.CommandExecutor.Execute(this.Project<ShopItemCategoryEditViewModel, ShopItemCategoryCreateCommand>(vm));

                return RedirectToAction("CreateItemCategory");
            }

            return View();
        }
        
        public ActionResult EditItem(string gameId)
        {
            ShopItemEditViewModel vm;

            try
            {
                if (String.IsNullOrEmpty(gameId)) throw new Exception();

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
        public ActionResult EditItem(ShopItemEditViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var uploadImage = Request.Files["uploadShopItemImage"];
                    if (uploadImage != null && uploadImage.ContentLength > 0) vm.Image = ImageManager.GetImageBytes(uploadImage);

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

        [HttpPost]
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
    }
}