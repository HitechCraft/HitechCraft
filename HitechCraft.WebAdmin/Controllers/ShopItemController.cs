using System.Text.RegularExpressions;
using HitechCraft.Core.DI;
using HitechCraft.Core.Entity;
using HitechCraft.Projector.Impl;

namespace HitechCraft.WebAdmin.Controllers
{
    using System.Web.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BL.CQRS.Command;
    using BL.CQRS.Query;
    using Manager;
    using Models;
    using PagedList;

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
        public ActionResult ItemsPartialList(int? page, string itemTitleFilter = "")
        {
            int currentPage = page ?? 1;

            var items = new EntityListQueryHandler<ShopItem, ShopItemViewModel>(Container)
                .Handle(new EntityListQuery<ShopItem, ShopItemViewModel>()
                {
                    Projector = Container.Resolve<IProjector<ShopItem, ShopItemViewModel>>()
                });

            if (!String.IsNullOrEmpty(itemTitleFilter))
                items =
                    items.Where(x => x.Name.Contains(itemTitleFilter)).ToList();

            return PartialView("_ShopItemListPartial", items.ToPagedList(currentPage, ItemsOnPage));
        }
        
        public ActionResult CreateItem()
        {
            ViewBag.Mods = GetMods().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = x.Name == "Vanilla"
            });

            ViewBag.Categories = GetCategories().Select(x => new SelectListItem()
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
                int gameId;

                if(string.IsNullOrEmpty(vm.GameId))
                    ModelState.AddModelError(String.Empty, "ID не может быть пустым");

                if(!int.TryParse(vm.GameId, out gameId) || !Regex.IsMatch(vm.GameId, @"[0-9]+\:[0-9]+"))
                    ModelState.AddModelError(String.Empty, "ID имеет неверный формат");

                var uploadImage = Request.Files["uploadShopItemImage"];

                if (uploadImage == null || uploadImage.ContentLength <= 0) ModelState.AddModelError(String.Empty, "Не выбрано изображение");

                if (ModelState.IsValid)
                {
                    vm.Image = ImageManager.GetImageBytes(uploadImage);

                    CommandExecutor.Execute(Project<ShopItemEditViewModel, ShopItemCreateCommand>(vm));

                    return RedirectToAction("CreateItem");
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }

            ViewBag.Mods = GetMods().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            ViewBag.Categories = GetCategories().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return View(vm);
        }
        
        public ActionResult ItemCategory()
        {
            ViewBag.Categories = GetCategories();

            return View();
        }

        [HttpPost]
        public ActionResult ItemCategory(ShopItemCategoryEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                CommandExecutor.Execute(Project<ShopItemCategoryEditViewModel, ShopItemCategoryCreateCommand>(vm));

                return RedirectToAction("ItemCategory");
            }

            ViewBag.Categories = GetCategories();

            return View();
        }
        
        public ActionResult EditItem(string gameId)
        {
            ShopItemEditViewModel vm;

            try
            {
                if (String.IsNullOrEmpty(gameId)) throw new Exception();

                vm = new EntityQueryHandler<ShopItem, ShopItemEditViewModel>(Container)
                   .Handle(new EntityQuery<ShopItem, ShopItemEditViewModel>()
                   {
                       Id = gameId,
                       Projector = Container.Resolve<IProjector<ShopItem, ShopItemEditViewModel>>()
                   });
            }
            catch (Exception)
            {
                return HttpNotFound();
            }

            ViewBag.Mods = GetMods().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = (x.Id == vm.ModificationId)
            });

            ViewBag.Categories = GetCategories().Select(x => new SelectListItem()
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

                    CommandExecutor.Execute(Project<ShopItemEditViewModel, ShopItemUpdateCommand>(vm));

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }

            ViewBag.Mods = GetMods().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = (x.Id == vm.ModificationId)
            });

            ViewBag.Categories = GetCategories().Select(x => new SelectListItem()
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
                    CommandExecutor.Execute(new ShopItemRemoveCommand()
                    {
                        GameId = gameId
                    });

                    return Json(new { status = "OK", message = "Предмет успешно удален" });
                }

                return Json(new { status = "NO", message = "Ошибка удаления предмета: Id имеет неверный формат" });
            }
            catch (Exception e)
            {
                return Json(new { status = "NO", message = "Ошибка удаления предмета: " + e.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteItemCategory(int categoryId)
        {
            try
            {
                if (categoryId > 0)
                {
                    CommandExecutor.Execute(new ShopItemCategoryRemoveCommand()
                    {
                        CategoryId = categoryId
                    });

                    return Json(new { status = "OK", message = "Категория успешно удалена" });
                }

                return Json(new { status = "NO", message = "Не указан Id категории" });
            }
            catch (Exception e)
            {
                return Json(new { status = "NO", message = "Ошибка удаления категории: " + e.Message });
            }
        }
        
        private ICollection<ModificationViewModel> GetMods()
        {
            return new EntityListQueryHandler<Modification, ModificationViewModel>(Container)
                .Handle(new EntityListQuery<Modification, ModificationViewModel>()
                {
                    Projector = Container.Resolve<IProjector<Modification, ModificationViewModel>>()
                });
        }

        private ICollection<ShopItemCategoryViewModel> GetCategories()
        {
            return new EntityListQueryHandler<ShopItemCategory, ShopItemCategoryViewModel>(Container)
                .Handle(new EntityListQuery<ShopItemCategory, ShopItemCategoryViewModel>()
                {
                    Projector = Container.Resolve<IProjector<ShopItemCategory, ShopItemCategoryViewModel>>()
                });
        }
    }
}