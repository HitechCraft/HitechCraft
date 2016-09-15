using HitechCraft.Core.DI;
using HitechCraft.Core.Entity;
using HitechCraft.DAL.Repository;
using HitechCraft.Projector.Impl;

namespace HitechCraft.BL.CQRS.Command.Base
{
    #region Using Directives

    

    #endregion

    public abstract class BaseCommandHandler<TCommand> : ICommandHandler<TCommand>
    {
        private readonly IContainer _container;

        protected BaseCommandHandler(IContainer container)
        {
            _container = container;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity<TEntity>
        {
            return _container.Resolve<IRepository<TEntity>>();
        }

        public IProjector<TEntity, TResult> GetProjector<TEntity, TResult>()
        {
            return _container.Resolve<IProjector<TEntity, TResult>>();
        }

        public abstract void Handle(TCommand command);
    }
}
