using System.IO;

namespace HitechCraft.WebAdmin.Controllers
{
    #region Using Directives

    using System.Web.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BL.CQRS.Command;
    using BL.CQRS.Query;
    using Manager;
    using Models;
    using PagedList;
    using System.Text.RegularExpressions;
    using Core.DI;
    using Core.Entity;
    using Projector.Impl;
    using Microsoft.Office.Interop.Excel;

    #endregion

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

        #region Actions

        [HttpPost]
        public JsonResult ExportToExcel()
        {
            try
            {
                var entities = new EntityListQueryHandler<ShopItem, ShopItemViewModel>(this.Container)
                    .Handle(new EntityListQuery<ShopItem, ShopItemViewModel>
                    {
                        Projector = this.Container.Resolve<IProjector<ShopItem, ShopItemViewModel>>()
                    }).ToArray();

                var xlApp = new Application();

                var missing = System.Reflection.Missing.Value;

                var xlWorkBook = xlApp.Workbooks.Add(System.Reflection.Missing.Value);
                var xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1); ;

                xlWorkSheet.Cells[1, 1] = "ID";
                xlWorkSheet.Cells[1, 2] = "GameId";
                xlWorkSheet.Cells[1, 3] = "Name";
                xlWorkSheet.Cells[1, 4] = "Price";
                xlWorkSheet.Cells[1, 5] = "Modification";
                xlWorkSheet.Cells[1, 6] = "Category";

                for (int i = 0; i < entities.Length; i++)
                {
                    xlWorkSheet.Cells[i + 2, 1] = entities[i].Id;
                    xlWorkSheet.Cells[i + 2, 2] = entities[i].GameId;
                    xlWorkSheet.Cells[i + 2, 3] = entities[i].Name;
                    xlWorkSheet.Cells[i + 2, 4] = entities[i].Price;
                    xlWorkSheet.Cells[i + 2, 5] = entities[i].ModificationName;
                    xlWorkSheet.Cells[i + 2, 6] = entities[i].CategoryName;
                }

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=ShopItem" + DateTime.Now.ToString("dd-MM-yy-HH:mm:ss") +".xlsx");

                using (MemoryStream myMemoryStream = new MemoryStream())
                {
                    xlWorkBook.SaveAs(myMemoryStream);
                    xlWorkBook.Close(true, missing, missing);
                    xlApp.Quit();

                    myMemoryStream.WriteTo(Response.OutputStream);

                    Response.Flush();
                    Response.End();
                }

                return Json(new { status = "OK", message = "Успешно!" });
            }
            catch (Exception e)
            {
                return Json(new { status = "NO", message = "Ошибка экспорта: " + e.Message });
            }
        }

        #endregion

        #region Private Methods

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

        #endregion
    }
}