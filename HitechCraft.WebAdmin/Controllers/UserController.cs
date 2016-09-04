using System;
using HitechCraft.BL.CQRS.Query;
using HitechCraft.Common.Projector;
using HitechCraft.DAL.Domain;
using HitechCraft.DAL.Repository.Specification;

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

        public ActionResult PlayerInfoPartial(string userName = "")
        {
            try
            {
                var player = new EntityListQueryHandler<Player, PlayerInfoViewModel>(this.Container)
                    .Handle(new EntityListQuery<Player, PlayerInfoViewModel>()
                    {
                        Projector = this.Container.Resolve<IProjector<Player, PlayerInfoViewModel>>(),
                        Specification = new PlayerByLoginSpec(userName)
                    });

                return PartialView("_PlayerInfoPartial", player.First());
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