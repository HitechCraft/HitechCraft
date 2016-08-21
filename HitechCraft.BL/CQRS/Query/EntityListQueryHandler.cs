namespace HitechCraft.BL.CQRS.Query
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using Common.CQRS.Query;
    using Common.DI;
    using Common.Entity;
    using Common.Repository;

    #endregion

    public class EntityListQueryHandler<TEntity, TResult> 
        : IQueryHandler<EntityListQuery<TEntity, TResult>, ICollection<TResult>> where TEntity : BaseEntity<TEntity>
    {
        private readonly IContainer _container;

        public EntityListQueryHandler(IContainer container)
        {
            this._container = container;
        }

        public ICollection<TResult> Handle(EntityListQuery<TEntity, TResult> query)
        {
            var entityRep = this._container.Resolve<IRepository<TEntity>>();
            
            if(query.Projector == null)
                throw new Exception("Для получения объектов необходима проекция сущностей");

            return entityRep.Query(query.Specification, query.Projector);
        }
    }
}
