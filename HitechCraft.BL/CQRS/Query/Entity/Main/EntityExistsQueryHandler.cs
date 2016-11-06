using HitechCraft.Core.Databases;

namespace HitechCraft.BL.CQRS.Query
{
    #region UsingDirectives
    
    using Core.DI;
    using DAL.Repository;
    using System.Linq;
    using HitechCraft.Core.Entity.Base;

    #endregion

    public class EntityExistsQueryHandler<TEntity> 
        : IQueryHandler<EntityExistsQuery<TEntity>, bool> where TEntity : BaseEntity<TEntity>
    {
        private readonly IContainer _container;

        public EntityExistsQueryHandler(IContainer container)
        {
            _container = container;
        }

        public bool Handle(EntityExistsQuery<TEntity> query)
        {
            var entityRep = _container.Resolve<IRepository<MySQLConnection, TEntity>>();
            
            return entityRep.Query(query.Specification).Any();
        }
    }
}
