namespace HitechCraft.BL.CQRS.Query
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using Core.DI;
    using Core.Entity;
    using DAL.Repository;
    using HitechCraft.BL.CQRS.Query.Base;
    using HitechCraft.Core.Entity.Base;

    #endregion

    public class TestListQueryHandler<TEntity, TResult> 
        : BaseQueryHandler<TestListQuery<TEntity, TResult>, ICollection<TResult>> where TEntity : BaseEntity<TEntity>
    {
        public TestListQueryHandler(IContainer container) : base(container)
        {
        }

        public override ICollection<TResult> Handle(TestListQuery<TEntity, TResult> query)
        {
            if (query.Projector == null)
                throw new Exception("Для получения объектов необходима проекция сущностей");

            var entityRep = this.GetRepository<TEntity>();
            
            return entityRep.Query(query.Specification, query.Projector);
        }
    }
}
