namespace HitechCraft.BL.CQRS.Query
{
    #region Using Directives

    using HitechCraft.Core.Entity.Base;
    using HitechCraft.Core.Repository.Specification;

    #endregion

    public class EntityCountQuery<TEntity> : IQuery<int> where TEntity : BaseEntity<TEntity>
    {
        public ISpecification<TEntity> Specification { get; set; }
    }
}
