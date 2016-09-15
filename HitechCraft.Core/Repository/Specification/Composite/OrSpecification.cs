using System;
using System.Linq.Expressions;
using HitechCraft.Core.Entity;

namespace HitechCraft.Core.Repository.Specification.Composite
{
    #region Using Directives

    

    #endregion

    /// <summary>
    /// Specification for logic combining of other specifications
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class OrSpecification<TEntity> : ISpecification<TEntity> where TEntity : BaseEntity<TEntity>
    {
        private readonly ISpecification<TEntity> _left;
        private readonly ISpecification<TEntity> _right;

        public OrSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right)
        {
            this._left = left;
            this._right = right;
        }

        public Expression<Func<TEntity, bool>> IsSatisfiedBy()
        {
            var param = Expression.Parameter(typeof(TEntity), "x");

            var body = Expression.OrElse(
                    Expression.Invoke(this._left.IsSatisfiedBy(), param),
                    Expression.Invoke(this._right.IsSatisfiedBy(), param)
                );

            var lambda = Expression.Lambda<Func<TEntity, bool>>(body, param);

            return lambda;
        }
    }
}
