using HitechCraft.Core.Databases;
using HitechCraft.Core.Projector;

namespace HitechCraft.BL.CQRS.Command.Base
{
    #region Using Directives

    using HitechCraft.Core.DI;
    using HitechCraft.Core.Entity;
    using HitechCraft.Core.Entity.Base;
    using HitechCraft.DAL.Repository;
    

    #endregion

    public abstract class BaseCommandHandler<TCommand> : ICommandHandler<TCommand>
    {
        private readonly IContainer _container;

        protected BaseCommandHandler(IContainer container)
        {
            _container = container;
        }

        public IRepository<TDataBase, TEntity> GetRepository<TDataBase, TEntity>() where TEntity : BaseEntity<TEntity> where TDataBase : IDataBase
        {
            return _container.Resolve<IRepository<TDataBase, TEntity>>();
        }

        public IRepository<MySQLConnection, TEntity> GetRepository<TEntity>() where TEntity : BaseEntity<TEntity>
        {
            return _container.Resolve<IRepository<MySQLConnection, TEntity>>();
        }

        public IProjector<TEntity, TResult> GetProjector<TEntity, TResult>()
        {
            return _container.Resolve<IProjector<TEntity, TResult>>();
        }

        public abstract void Handle(TCommand command);
    }
}
