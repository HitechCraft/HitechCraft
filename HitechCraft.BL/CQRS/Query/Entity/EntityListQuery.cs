using HitechCraft.Core.Entity.Base;
using HitechCraft.Core.Repository.Specification;

namespace HitechCraft.BL.CQRS.Query
{
    #region Using Directives

    using System.Collections.Generic;
    using Core.Entity;
    using Projector.Impl;

    #endregion

    public class EntityListQuery<TEntity, TResult> : IQuery<ICollection<TResult>> where TEntity : BaseEntity<TEntity>
    {
        public ISpecification<TEntity> Specification { get; set; }

        public IProjector<TEntity, TResult> Projector { get; set; }
    }
}
