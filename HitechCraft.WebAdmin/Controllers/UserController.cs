using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace HitechCraft.WebAdmin.Controllers
{
    using System.Web.Mvc;
    using Common.DI;
    using System.Linq;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using BL.CQRS.Command;
    using BL.CQRS.Query;
    using Common.Models.Enum;
    using Common.Projector;
    using DAL.Domain;
    using DAL.Repository.Specification;
    using Manager;
    using Models.User;
    using PagedList;

    public class UserController : BaseController
    {
        private ApplicationDbContext _context;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public RoleManager<IdentityRole> RoleManager
        {
            get
            {
                if (_roleManager == null)
                    _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(this.Context));
                return _roleManager;
            }
        }

        public int UsersOnPage => 10;

        public ApplicationDbContext Context
        {
            get
            {
                if (_context == null)
                {
                    _context = new ApplicationDbContext();
                }

                return _context;
            }
        }
        public UserController(IContainer container) : base(container)
        {
        }

        // GET: User
        public ActionResult Index()
        {
            ViewBag.UsersOnPage = this.UsersOnPage;

            return View();
        }
        
        public ActionResult UserPartialList(int? page, string userNameFilter = "")
        {
            int currentPage = page ?? 1;

            var users = Context.Users.ToList();

            if (!String.IsNullOrEmpty(userNameFilter))
                users =
                    users.Where(x => x.UserName.Contains(userNameFilter) || x.Email.Contains(userNameFilter)).ToList();

            return PartialView("_UserPartialList", users.ToPagedList(currentPage, this.UsersOnPage));
        }
        
        public string GetSkinImage(Gender? gender, string userName)
        {
            var playerSkinVm = new PlayerSkinQueryHandler<PlayerSkinViewModel>(Container)
                .Handle(new PlayerSkinQuery<PlayerSkinViewModel>()
                {
                    UserName = userName,
                    Gender = gender != null ? gender.Value : Gender.Male,
                    Projector = Container.Resolve<IProjector<PlayerSkin, PlayerSkinViewModel>>()
                });

            return Convert.ToBase64String(playerSkinVm.Image);
        }

        public ActionResult PlayerInfoPartial(string userName = "")
        {
            try
            {
                var playerInfo = new EntityListQueryHandler<Currency, PlayerInfoViewModel>(Container)
                    .Handle(new EntityListQuery<Currency, PlayerInfoViewModel>()
                    {
                        Projector = Container.Resolve<IProjector<Currency, PlayerInfoViewModel>>(),
                        Specification = new CurrencyByPlayerNameSpec(userName)
                    });

                return PartialView("_PlayerInfoPartial", playerInfo.First());
            }
            catch (Exception e)
            {
                ViewBag.NoPlayer = true;
                return PartialView("_PlayerInfoPartial");
            }
        }

        public ActionResult Edit(string playerName)
        {
            try
            {
                var vm = new EntityListQueryHandler<Currency, PlayerInfoViewModel>(Container)
                .Handle(new EntityListQuery<Currency, PlayerInfoViewModel>()
                {
                    Specification = new CurrencyByPlayerNameSpec(playerName),
                    Projector = Container.Resolve<IProjector<Currency, PlayerInfoViewModel>>()
                }).First();

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

                return View(vm);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Edit(PlayerInfoViewModel vm)
        {
            if(vm.Gonts < 0 || vm.Rubles < 0) ModelState.AddModelError(String.Empty, "Величина валюты должна быть больше 0!");

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

            if (ModelState.IsValid)
            {
                try
                {
                    CommandExecutor.Execute(new PlayerInfoUpdateCommand()
                    {
                        Name = vm.Name,
                        Gonts = vm.Gonts,
                        Rubles = vm.Rubles,
                        Gender = vm.Gender
                    });

                    return RedirectToAction("Edit", new { playerName = vm.Name });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(String.Empty, e.Message);
                    return View(vm);
                }
            }

            return View(vm);
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult UploadSkinImage(string playerName)
        {
            var uploadImage = Request.Files["uploadSkinImage"];

            byte[] bytes;

            var errors = CheckPlayerSkin(uploadImage, out bytes);

            if (!errors.Any())
            {
                CommandExecutor.Execute(new PlayerSkinCreateOrUpdateCommand()
                {
                    PlayerName = playerName,
                    Image = bytes
                });

                return Json(new { status = "OK", data = "" });
            }
            else
            {
                return Json(new { status = "NO", data = errors });
            }
        }

        public bool IsPlayerSkinExists(string playerName)
        {
            return new PlayerSkinExistsQueryHandler(this.Container)
                .Handle(new PlayerSkinExistsQuery()
                {
                    UserName = playerName
                });
        }

        [HttpPost]
        public JsonResult RemovePlayerSkin(string playerName)
        {
            try
            {
                if (!IsPlayerSkinExists(playerName))
                {
                    return Json(new
                    {
                        status = JsonStatus.NO,
                        message = "Скин пользователя не загружен"
                    }, JsonRequestBehavior.AllowGet);
                }

                new PlayerSkinRemoveCommandHandler(this.Container)
                    .Handle(new PlayerSkinRemoveCommand()
                    {
                        PlayerName = playerName
                    });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = JsonStatus.NO,
                    message = e.Message
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = JsonStatus.YES,
                message = "Скин успешно сменен на стандартный"
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Только для получения аватара в списке
        /// </summary>
        /// <param name="playerName">Имя игрока</param>
        /// <returns></returns>
        public Gender GetPlayerGender(string playerName)
        {
            try
            {
                return new EntityListQueryHandler<Currency, PlayerInfoViewModel>(this.Container)
                    .Handle(new EntityListQuery<Currency, PlayerInfoViewModel>()
                    {
                        Projector = this.Container.Resolve<IProjector<Currency, PlayerInfoViewModel>>(),
                        Specification = new CurrencyByPlayerNameSpec(playerName)
                    }).First().Gender;
            }
            catch (Exception)
            {
                return Gender.Male;
            }
        }

        public ActionResult FixUserAccount(string userName, Gender gender, string email)
        {
            try
            {
                if (!new EntityListQueryHandler<Currency, PlayerInfoViewModel>(this.Container)
                    .Handle(new EntityListQuery<Currency, PlayerInfoViewModel>()
                    {
                        Projector = this.Container.Resolve<IProjector<Currency, PlayerInfoViewModel>>(),
                        Specification = new CurrencyByPlayerNameSpec(userName)
                    }).Any())
                {
                    this.CommandExecutor.Execute(new PlayerRegisterCreateCommand()
                    {
                        Name = userName,
                        Gender = gender,
                        Email = email,
                        ReferId = String.Empty
                    });
                }
            }
            catch (Exception e)
            {
            }

            return RedirectToAction("Index");
        }

        public bool CheckUserGameAccount(string userName)
        {
            return new EntityListQueryHandler<Currency, PlayerInfoViewModel>(Container)
                .Handle(new EntityListQuery<Currency, PlayerInfoViewModel>()
                {
                    Specification = new CurrencyByPlayerNameSpec(userName),
                    Projector = Container.Resolve<IProjector<Currency, PlayerInfoViewModel>>()
                }).Any();
        }

        public ActionResult UserRoles(string userName)
        {
            IEnumerable<string> roleNames = null;

            try
            {
                roleNames = this.Context.Users.First(x => x.UserName == userName).Roles.Select(x => this.Context.Roles.First(y => y.Id == x.RoleId).Name);
            }
            catch (Exception)
            {
                return PartialView("_UserRolesPartial", roleNames);
            }

            return PartialView("_UserRolesPartial", roleNames);
        }

        private List<string> CheckPlayerSkin(HttpPostedFileBase skinFile, out byte[] bytes)
        {
            var errors = new List<string>();
            bytes = ImageManager.GetImageBytes(skinFile);

            if (skinFile == null)
            {
                errors.Add("Файл скина не выбран");

                return errors;
            }

            var fileType = skinFile.ContentType;
            var allowedTypes = new List<string>() { "image/png" };

            if (!allowedTypes.Contains(fileType))
            {
                errors.Add("Скин может быть в формате .png");

                return errors;
            }

            var image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(bytes));

            if (image.Width <= 64 && (image.Width / image.Height != 2 && image.Width / image.Height != 1))
            {
                errors.Add("Скины должны быть формата 1:1 или 2:1 (например, 64x64 или 64x32)");
            }

            if (image.Width > 64 && image.Width / image.Height != 2)
            {
                errors.Add("HD скины должны быть формата 2:1 (например, 1024x512)");
            }

            if (image.Width > 1024)
            {
                errors.Add("Максимальный размер скина - 1024x512");
            }

            if (skinFile.ContentLength / 1048576 > 1)
            {
                errors.Add("Максимальный размер файла - 1 МБ");
            }

            return errors;
        }
    }
}