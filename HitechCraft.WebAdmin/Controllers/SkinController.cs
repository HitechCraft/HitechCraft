using System;
using System.Collections.Generic;
using System.Linq;
using HitechCraft.BL.CQRS.Command;
using HitechCraft.BL.CQRS.Query;
using HitechCraft.Common.Core;
using HitechCraft.Common.Models.Enum;
using HitechCraft.Common.Projector;
using HitechCraft.DAL.Domain;
using HitechCraft.DAL.Domain.Extentions;
using HitechCraft.WebAdmin.Manager;
using HitechCraft.WebAdmin.Models;
using MySql.Data.MySqlClient.Properties;
using PagedList;

namespace HitechCraft.WebAdmin.Controllers
{
    using System.Web.Mvc;
    using HitechCraft.Common.DI;

    public class SkinController : BaseController
    {
        public int SkinsOnPage => 8;

        public SkinController(IContainer container) : base(container)
        {
        }

        // GET: Skin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SkinPartialList(int? page, string skinTitleFilter = "")
        {
            int currentPage = page ?? 1;

            var skins = new EntityListQueryHandler<Skin, SkinViewModel>(Container)
                .Handle(new EntityListQuery<Skin, SkinViewModel>()
                {
                    Projector = Container.Resolve<IProjector<Skin, SkinViewModel>>()
                });
            
            if (!String.IsNullOrEmpty(skinTitleFilter))
                skins =
                    skins.Where(x => x.Name.Contains(skinTitleFilter)).ToList();
            
            return PartialView("_SkinPartialList", skins.ToPagedList(currentPage, this.SkinsOnPage));
        }
        
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.Gender = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Мужской",
                    Value = ((int)Gender.Male).ToString(),
                    Selected = true
                },

                new SelectListItem()
                {
                    Text = "Женский",
                    Value = ((int)Gender.Female).ToString()
                }
            };

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(SkinEditViewModel vm)
        {
            var uploadFile = Request.Files["uploadSkinImage"];

            if (uploadFile == null) ModelState.AddModelError(String.Empty, "Выберите изображение");

            if (ModelState.IsValid)
            {
                this.CommandExecutor.Execute(new SkinCreateCommand()
                {
                    Name = vm.Name,
                    Image = ImageManager.GetImageBytes(uploadFile),
                    Gender = vm.Gender
                });

                return RedirectToAction("Create");
            }

            ViewBag.Gender = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Мужской",
                    Value = ((int)Gender.Male).ToString(),
                    Selected = true
                },

                new SelectListItem()
                {
                    Text = "Женский",
                    Value = ((int)Gender.Female).ToString()
                }
            };

            return View();
        }

        [HttpPost]
        public ActionResult CheckExistingSkin()
        {
            var uploadedFile = Request.Files["uploadSkinImage"];

            var bytes = ImageManager.GetImageBytes(uploadedFile);

            var skins = new EntityListQueryHandler<Skin, SkinViewModel>(this.Container)
                .Handle(new EntityListQuery<Skin, SkinViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<Skin, SkinViewModel>>()
                }).Where(x => x.Image.IsEquals(bytes));

            if (skins.Any())
            {
                return PartialView("_SkinDuplicatedPartial", skins.First());
            }

            return this.Content("OK");
        }

        [HttpPost]
        public ActionResult CheckExistingSkinByBase64(string base64)
        {
            var bytes = HashManager.GetBase64Bytes(base64);

            var skins = new EntityListQueryHandler<Skin, SkinViewModel>(this.Container)
                .Handle(new EntityListQuery<Skin, SkinViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<Skin, SkinViewModel>>()
                }).Where(x => x.Image.IsEquals(bytes));

            if (skins.Any())
            {
                return PartialView("_SkinDuplicatedPartial", skins.First());
            }

            return this.Content("OK");
        }

        [HttpPost]
        public JsonResult Delete(int? id)
        {
            try
            {
                this.CommandExecutor.Execute(new SkinRemoveCommand()
                {
                    Id = id.Value
                });

                return Json(new { status = "OK", message = "Скин удален" });
            }
            catch (Exception e)
            {
                return Json(new { status = "NO", message = "Ошибка удаления скина: " + e.Message });
            }
        }
    }
}