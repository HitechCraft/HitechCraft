namespace HitechCraft.BL.CQRS.Query
{
    #region Using Directives

    using System.Collections.Generic;
    using Core.Entity;
    using HitechCraft.Core.Repository.Specification;
    using HitechCraft.Core.Projector;
    using HitechCraft.Core.Entity.Base;


    #endregion

    public class TestListQuery<TEntity, TResult> : IQuery<ICollection<TResult>> where TEntity : BaseEntity<TEntity>
    {
        public ISpecification<TEntity> Specification { get; set; }

        public IProjector<TEntity, TResult> Projector { get; set; }
    }
}
