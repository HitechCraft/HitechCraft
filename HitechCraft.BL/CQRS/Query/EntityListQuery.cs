namespace HitechCraft.BL.CQRS.Query
{
    #region Using Directives

    using System.Collections.Generic;
    using Common.CQRS.Query;
    using Common.Entity;
    using Common.Projector;
    using Common.Repository.Specification;

    #endregion

    public class EntityListQuery<TEntity, TResult> : IQuery<ICollection<TResult>> where TEntity : BaseEntity<TEntity>
    {
        public ISpecification<TEntity> Specification { get; set; }

        public IProjector<TEntity, TResult> Projector { get; set; }
    }
}
