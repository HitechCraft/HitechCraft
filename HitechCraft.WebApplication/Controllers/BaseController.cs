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

        private Player CurrentPlayer;

        private Currency CurrentCurrency;

        #endregion

        public IContainer Container { get; set; }

        public ICommandExecutor CommandExecutor { get; set; }

        public ICurrentUser CurrentUser { get; set; }

        public Player Player { get; }

        public Currency Currency { get; }

        public BaseController(IContainer container)
        {
            this.Container = container;
            this.CommandExecutor = this.Container.Resolve<ICommandExecutor>();

            this.CurrentUser = this.Container.Resolve<ICurrentUser>();

            this.Player = this.GetCurrentPlayer();

            this.Currency = this.GetCurrency();
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

        #region Entity Queries

        public TResult EntityQuery<TEntity, TResult>(int entityId, IProjector<TEntity, TResult> projector)
            where TEntity : BaseEntity<TEntity>
        {
            return new EntityQueryHandler<TEntity, TResult>(this.Container)
                .Handle(new EntityQuery<TEntity, TResult>()
                {
                    Id = entityId,
                    Projector = projector
                });
        }

        public ICollection<TResult> EntityListQuery<TEntity, TResult>(IProjector<TEntity, TResult> projector)
            where TEntity : BaseEntity<TEntity>
        {
            return this.EntityListQuery(null, projector);
        }

        public ICollection<TResult> EntityListQuery<TEntity, TResult>(ISpecification<TEntity> specification, IProjector<TEntity, TResult> projector)
            where TEntity : BaseEntity<TEntity>
        {
            return new EntityListQueryHandler<TEntity, TResult>(this.Container)
                .Handle(new EntityListQuery<TEntity, TResult>()
                {
                    Specification = specification,
                    Projector = projector
                });
        }

        #endregion
    }
}