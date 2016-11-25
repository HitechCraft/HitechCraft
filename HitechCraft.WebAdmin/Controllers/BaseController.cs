using HitechCraft.BL.CQRS.Command.Base;
using HitechCraft.Core.DI;
using HitechCraft.Core.Projector;


namespace HitechCraft.WebAdmin.Controllers
{
    #region Using Directives

    using System.Web.Mvc;
    using Models;
    using Microsoft.AspNet.Identity;


    #endregion

    public class BaseController : Controller
    {
        #region Private Fields

        private ApplicationDbContext _context;

        #endregion
        
        public IContainer Container { get; set; }

        public ICommandExecutor CommandExecutor { get; set; }

        public ApplicationDbContext Context => _context ?? (_context = new ApplicationDbContext());

        public ApplicationUser Admin => this.Context.Users.Find(User.Identity.GetUserId());

        public BaseController(IContainer container)
        {
            this.Container = container;
            this.CommandExecutor = this.Container.Resolve<ICommandExecutor>();
        }

        public TResult Project<TSource, TResult>(TSource source)
        {
            return this.Container.Resolve<IProjector<TSource, TResult>>().Project(source);
        }
    }
}