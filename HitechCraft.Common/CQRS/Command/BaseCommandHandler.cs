using HitechCraft.Common.Projector;

namespace HitechCraft.Common.CQRS.Command
{
    #region Using Directives

    using DI;
    using Entity;
    using Repository;

    #endregion

    public abstract class BaseCommandHandler<TCommand> : ICommandHandler<TCommand>
    {
        private readonly IContainer _container;

        protected BaseCommandHandler(IContainer container)
        {
            this._container = container;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity<TEntity>
        {
            return this._container.Resolve<IRepository<TEntity>>();
        }

        public IProjector<TEntity, TResult> GetProjector<TEntity, TResult>()
        {
            return this._container.Resolve<IProjector<TEntity, TResult>>();
        }

        public abstract void Handle(TCommand command);
    }
}
