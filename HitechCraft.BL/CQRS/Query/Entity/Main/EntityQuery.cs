using HitechCraft.Core.Entity.Base;
using HitechCraft.Core.Projector;

namespace HitechCraft.BL.CQRS.Query
{
    #region Using Directives

    using Core.Entity;
    

    #endregion

    public class EntityQuery<TEntity, TResult> : IQuery<TResult> where TEntity : BaseEntity<TEntity>
    {
        public object Id { get; set; }

        public IProjector<TEntity, TResult> Projector { get; set; }
    }
}
