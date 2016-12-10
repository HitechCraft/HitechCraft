namespace HitechCraft.BL.CQRS.Query.Base
{
    #region Using Directives

    using HitechCraft.Core.Databases;
    using HitechCraft.Core.DI;
    using HitechCraft.Core.Entity.Base;
    using HitechCraft.Core.Projector;
    using HitechCraft.DAL.Repository;
    using System;

    #endregion

    public abstract class BaseQueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly IContainer _container;

        public BaseQueryHandler(IContainer container)
        {
            this._container = container;
        }

        public IRepository<TDataBase, TEntity> GetRepository<TDataBase, TEntity>() where TEntity : BaseEntity<TEntity> where TDataBase : IDataBase
        {
            return _container.Resolve<IRepository<TDataBase, TEntity>>();
        }

        public IRepository<MySQLConnection, TEntity> GetRepository<TEntity>() where TEntity : BaseEntity<TEntity>
        {
            return _container.Resolve<IRepository<MySQLConnection, TEntity>>();
        }

        public IProjector<TEntity, TResult> GetProjector<TEntity>()
        {
            return _container.Resolve<IProjector<TEntity, TResult>>();
        }

        public abstract TResult Handle(TQuery query);
    }
}
