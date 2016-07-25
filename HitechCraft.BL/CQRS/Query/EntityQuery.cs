namespace HitechCraft.BL.CQRS.Query
{
    #region Using Directives

    using Common.CQRS.Query;
    using Common.Entity;
    using Common.Projector;

    #endregion

    public class EntityQuery<TEntity, TResult> : IQuery<TResult> where TEntity : BaseEntity<TEntity>
    {
        public int Id { get; set; }

        public IProjector<TEntity, TResult> Projector { get; set; }
    }
}
