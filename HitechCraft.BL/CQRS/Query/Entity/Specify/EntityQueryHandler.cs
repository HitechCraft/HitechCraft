using HitechCraft.Core.Databases;

namespace HitechCraft.BL.CQRS.Query.Specify
{
    #region UsingDirectives

    using System;
    using Core.DI;
    using Core.Entity;
    using DAL.Repository;
    using HitechCraft.Core.Entity.Base;

    #endregion

    public class EntityQueryHandler<TEntity, TResult, TDataBase> 
        : IQueryHandler<EntityQuery<TEntity, TResult>, TResult> where TEntity : BaseEntity<TEntity> where TDataBase : IDataBase
    {
        private readonly IContainer _container;

        public EntityQueryHandler(IContainer container)
        {
            _container = container;
        }

        public TResult Handle(EntityQuery<TEntity, TResult> query)
        {
            var entityRep = _container.Resolve<IRepository<TDataBase, TEntity>>();

            if(query.Projector == null)
                throw new Exception("Для получения объекта необходима проекция сущностей");

            return query.Projector.Project(entityRep.GetEntity(query.Id));
        }
    }
}
