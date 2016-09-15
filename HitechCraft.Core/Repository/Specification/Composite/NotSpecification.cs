using System;
using System.Linq.Expressions;
using HitechCraft.Core.Entity;

namespace HitechCraft.Core.Repository.Specification.Composite
{
    #region Using Directives

    

    #endregion

    public class NotSpecification<TEntity> : ISpecification<TEntity> where TEntity : BaseEntity<TEntity>
    {
        private readonly ISpecification<TEntity> _wrapped;

        public NotSpecification(ISpecification<TEntity> wrapped)
        {
            this._wrapped = wrapped;
        }

        public Expression<Func<TEntity, bool>> IsSatisfiedBy()
        {
            var param = Expression.Parameter(typeof(TEntity), "x");
            var body = Expression.Not(Expression.Invoke(this._wrapped.IsSatisfiedBy(), param));

            return Expression.Lambda<Func<TEntity, bool>>(body, param);
        }
    }
}
