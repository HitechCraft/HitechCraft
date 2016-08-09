namespace HitechCraft.BL.CQRS.Query
{
    #region UsingDirectives

    using System;
    using Common.CQRS.Query;
    using Common.DI;
    using Common.Entity;
    using Common.Repository;

    #endregion

    public class EntityQueryHandler<TEntity, TResult> 
        : IQueryHandler<EntityQuery<TEntity, TResult>, TResult> where TEntity : BaseEntity<TEntity>
    {
        private readonly IContainer _container;

        public EntityQueryHandler(IContainer container)
        {
            _container = container;
        }

        public TResult Handle(EntityQuery<TEntity, TResult> query)
        {
            var entityRep = this._container.Resolve<IRepository<TEntity>>();

            if(query.Projector == null)
                throw new Exception("Для получения объекта необходима проекция сущностей");

            return query.Projector.Project(entityRep.GetEntity(query.Id));
        }
    }
}
