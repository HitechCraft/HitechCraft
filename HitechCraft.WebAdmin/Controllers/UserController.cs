using System;
using System.Collections.Generic;
using HitechCraft.BL.CQRS.Command;
using HitechCraft.BL.CQRS.Query;
using HitechCraft.Common.Models.Enum;
using HitechCraft.Common.Projector;
using HitechCraft.DAL.Domain;
using HitechCraft.DAL.Repository.Specification;
using HitechCraft.WebAdmin.Models.User;

namespace HitechCraft.WebAdmin.Controllers
{
    using System.Web.Mvc;
    using Common.DI;
    using System.Linq;
    using Models;

    public class UserController : BaseController
    {
        private ApplicationDbContext _context;

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
            return View();
        }
        
        public ActionResult UserPartialList(string userNameFilter = "")
        {
            var users = this.Context.Users.ToList();

            if (!String.IsNullOrEmpty(userNameFilter))
                users =
                    users.Where(x => x.UserName.Contains(userNameFilter) || x.Email.Contains(userNameFilter)).ToList();

            return PartialView("_UserPartialList", users);
        }
        
        public string GetSkinImage(Gender? gender, string userName)
        {
            var playerSkinVm = new PlayerSkinQueryHandler<PlayerSkinViewModel>(this.Container)
                .Handle(new PlayerSkinQuery<PlayerSkinViewModel>()
                {
                    UserName = userName,
                    Gender = gender != null ? gender.Value : Gender.Male,
                    Projector = this.Container.Resolve<IProjector<PlayerSkin, PlayerSkinViewModel>>()
                });

            return Convert.ToBase64String(playerSkinVm.Image);
        }

        public ActionResult PlayerInfoPartial(string userName = "")
        {
            try
            {
                var playerInfo = new EntityListQueryHandler<Currency, PlayerInfoViewModel>(this.Container)
                    .Handle(new EntityListQuery<Currency, PlayerInfoViewModel>()
                    {
                        Projector = this.Container.Resolve<IProjector<Currency, PlayerInfoViewModel>>(),
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
                var vm = new EntityListQueryHandler<Currency, PlayerInfoViewModel>(this.Container)
                .Handle(new EntityListQuery<Currency, PlayerInfoViewModel>()
                {
                    Specification = new CurrencyByPlayerNameSpec(playerName),
                    Projector = this.Container.Resolve<IProjector<Currency, PlayerInfoViewModel>>()
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
                    this.CommandExecutor.Execute(new PlayerInfoUpdateCommand()
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
    }
}