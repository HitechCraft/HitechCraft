namespace HitechCraft.Common.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using Entity;
    using Composite;

    #endregion

    /// <summary>
    /// Base abstract specification
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseSpecification<TEntity> : ISpecification<TEntity> 
        where TEntity : BaseEntity<TEntity>
    {
        public abstract Expression<Func<TEntity, bool>> IsSatisfiedBy();
        
        public static ISpecification<TEntity> operator &(
            ISpecification<TEntity> specLeft, BaseSpecification<TEntity> specRight)
        {
            return new AndSpecification<TEntity>(specLeft, specRight);
        }

        public static ISpecification<TEntity> operator |(
            ISpecification<TEntity> specLeft, BaseSpecification<TEntity> specRight)
        {
            return new OrSpecification<TEntity>(specLeft, specRight);
        }

        public static ISpecification<TEntity> operator !(BaseSpecification<TEntity> wrapped)
        {
            return new NotSpecification<TEntity>(wrapped);
        }
    }
}
