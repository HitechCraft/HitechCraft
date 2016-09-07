using System;
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
        
        public ActionResult UserPartialList()
        {
            var users = this.Context.Users.ToList();

            return PartialView("_UserPartialList", users);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetSkinImage(Gender? gender, string userName = "")
        {
            var playerSkinVm = new PlayerSkinQueryHandler<PlayerSkinViewModel>(this.Container)
                .Handle(new PlayerSkinQuery<PlayerSkinViewModel>()
                {
                    UserName = userName,
                    Gender = gender != null ? gender.Value : Gender.Male,
                    Projector = this.Container.Resolve<IProjector<PlayerSkin, PlayerSkinViewModel>>()
                });

            return File(playerSkinVm.Image, "image/png");
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

        public ActionResult Edit(string userId)
        {
            return View();
        }
    }
}