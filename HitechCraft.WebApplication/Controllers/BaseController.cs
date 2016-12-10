namespace HitechCraft.WebApplication.Controllers
{
    #region Using Directives

    using System.Web.Mvc;
    using BL.CQRS.Query;
    using Ninject.Current;
    using HitechCraft.BL.CQRS.Command.Base;
    using HitechCraft.Core.DI;
    using HitechCraft.Core.Entity;
    using HitechCraft.Core.Models.Enum;
    using HitechCraft.Core.Projector;
    using HitechCraft.Core.Repository.Specification.PrivateMessage;

    using HitechCraft.WebApplication.Models;
    using HitechCraft.BL.CQRS.Query.Base;

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

        public IQueryExecutor Query { get; set; }

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

        public int NewMessagesCount
        {
            get
            {
                if (this.Player != null)
                {
                    return this.GetNewMessagesCount();
                }

                return 0;
            }
        }

        public BaseController(IContainer container)
        {
            this.Container = container;
            this.CommandExecutor = this.Container.Resolve<ICommandExecutor>();
            this.Query = this.Container.Resolve<IQueryExecutor>();

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

        private int GetNewMessagesCount()
        {
            return new EntityListQueryHandler<PrivateMessage, PrivateMessageViewModel>(this.Container)
                .Handle(new EntityListQuery<PrivateMessage, PrivateMessageViewModel>()
                {
                    Specification = new RecipientPrivateMessageByTypeSpec(this.Player.Name, PMType.New),
                    Projector = this.Container.Resolve<IProjector<PrivateMessage, PrivateMessageViewModel>>()
                }).Count;
        }
    }
}