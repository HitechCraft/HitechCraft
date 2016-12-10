namespace HitechCraft.BL.CQRS.Query
{
    #region Using Directives

    using System;
    using Core.DI;
    using DAL.Repository;
    using HitechCraft.Core.Entity.Base;
    using HitechCraft.Core.Databases;

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
            var entityRep = _container.Resolve<IRepository<MySQLConnection, TEntity>>();

            if(query.Projector == null)
                throw new Exception("Для получения объекта необходима проекция сущностей");

            return query.Projector.Project(entityRep.GetEntity(query.Id));
        }
    }
}
