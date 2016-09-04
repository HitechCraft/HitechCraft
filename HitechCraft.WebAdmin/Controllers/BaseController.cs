namespace HitechCraft.WebAdmin.Controllers
{
    #region Using Directives

    using System.Web.Mvc;
    using System.Collections.Generic;
    using BL.CQRS.Query;
    using Common.CQRS.Command;
    using Common.DI;
    using Common.Entity;
    using Common.Projector;
    using Common.Repository.Specification;
    using DAL.Domain;

    #endregion

    public class BaseController : Controller
    {
        #region Private Fields

        private Player _currentPlayer;

        private Currency _currentCurrency;

        private int? _newMessagesCount;

        #endregion

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