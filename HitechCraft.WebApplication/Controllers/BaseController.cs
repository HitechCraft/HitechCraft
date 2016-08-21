namespace HitechCraft.WebApplication.Controllers
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
    using Ninject.Current;

    #endregion

    public class BaseController : Controller
    {
        #region Private Fields

        private Player _currentPlayer;

        private Currency _currentCurrency;

        #endregion

        public IContainer Container { get; set; }

        public ICommandExecutor CommandExecutor { get; set; }

        public ICurrentUser CurrentUser { get; set; }

        public Player Player
        {
            get
            {
                if(this._currentPlayer == null)
                {
                    this._currentPlayer = this.GetCurrentPlayer();
                }

                return this._currentPlayer;
            }
        }

        public Currency Currency
        {
            get
            {
                if (this._currentCurrency == null)
                {
                    this._currentCurrency = this.GetCurrency();
                }

                return this._currentCurrency;
            }
        }

        public BaseController(IContainer container)
        {
            this.Container = container;
            this.CommandExecutor = this.Container.Resolve<ICommandExecutor>();

            this.CurrentUser = this.Container.Resolve<ICurrentUser>();
        }

        public TResult Project<TSource, TResult>(TSource source)
        {
            return this.Container.Resolve<IProjector<TSource, TResult>>().Project(source);
        }

        private Player GetCurrentPlayer()
        {
            try
            {
                var currentPlayer = new PlayerByLoginQueryHandler(this.Container)
                    .Handle(new PlayerByLoginQuery<Player>()
                    {
                        Login = this.CurrentUser.Login
                    });

                return currentPlayer;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        private Currency GetCurrency()
        {
            try
            {
                var currency = new CurrencyByPlayerNameQueryHandler(this.Container)
                    .Handle(new CurrencyByPlayerNameQuery()
                    {
                        PlayerName = this.CurrentUser.Login
                    });

                return currency;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}