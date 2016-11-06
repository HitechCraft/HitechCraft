using HitechCraft.Core.Databases;

namespace HitechCraft.BL.CQRS.Query.Specify
{
    #region UsingDirectives
    
    using Core.DI;
    using DAL.Repository;
    using System.Linq;
    using HitechCraft.Core.Entity.Base;

    #endregion

    public class EntityCountQueryHandler<TEntity, TDataBase> 
        : IQueryHandler<EntityCountQuery<TEntity>, int> where TEntity : BaseEntity<TEntity> where TDataBase : IDataBase
    {
        private readonly IContainer _container;

        public EntityCountQueryHandler(IContainer container)
        {
            _container = container;
        }

        public int Handle(EntityCountQuery<TEntity> query)
        {
            var entityRep = _container.Resolve<IRepository<TDataBase, TEntity>>();
            
            return entityRep.Query(query.Specification).Count;
        }
    }
}
