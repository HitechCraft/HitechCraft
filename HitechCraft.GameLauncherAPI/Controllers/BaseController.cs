using HitechCraft.Core.Projector;

namespace HitechCraft.GameLauncherAPI.Controllers
{
    using System.Web.Mvc;
    using HitechCraft.BL.CQRS.Command.Base;
    
    using Core.DI;

    public class BaseController : Controller
    {
        public IContainer Container { get; set; }

        public ICommandExecutor CommandExecutor { get; set; }
        
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