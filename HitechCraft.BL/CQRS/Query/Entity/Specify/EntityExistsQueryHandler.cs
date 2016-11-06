using HitechCraft.Core.Databases;

namespace HitechCraft.BL.CQRS.Query.Specify
{
    #region UsingDirectives
    
    using Core.DI;
    using DAL.Repository;
    using System.Linq;
    using HitechCraft.Core.Entity.Base;

    #endregion

    public class EntityExistsQueryHandler<TEntity, TDataBase> 
        : IQueryHandler<EntityExistsQuery<TEntity>, bool> where TEntity : BaseEntity<TEntity> where TDataBase : IDataBase
    {
        private readonly IContainer _container;

        public EntityExistsQueryHandler(IContainer container)
        {
            _container = container;
        }

        public bool Handle(EntityExistsQuery<TEntity> query)
        {
            var entityRep = _container.Resolve<IRepository<TDataBase, TEntity>>();
            
            return entityRep.Query(query.Specification).Any();
        }
    }
}
