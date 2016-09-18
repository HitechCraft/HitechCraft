namespace HitechCraft.BL.CQRS.Query
{
    #region UsingDirectives
    
    using Core.DI;
    using DAL.Repository;
    using System.Linq;
    using HitechCraft.Core.Entity.Base;

    #endregion

    public class EntityCountQueryHandler<TEntity> 
        : IQueryHandler<EntityCountQuery<TEntity>, int> where TEntity : BaseEntity<TEntity>
    {
        private readonly IContainer _container;

        public EntityCountQueryHandler(IContainer container)
        {
            _container = container;
        }

        public int Handle(EntityCountQuery<TEntity> query)
        {
            var entityRep = _container.Resolve<IRepository<TEntity>>();
            
            return entityRep.Query(query.Specification).Count;
        }
    }
}
