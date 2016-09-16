using System;
using System.Linq.Expressions;
using HitechCraft.Core.Entity;
using HitechCraft.Core.Entity.Base;

namespace HitechCraft.Core.Repository.Specification
{
    #region Using Directives

    

    #endregion

    public interface ISpecification<TEntity> where TEntity : BaseEntity<TEntity>
    {
        /// <summary>
        /// Returning specific expression
        /// </summary>
        /// <returns></returns>
        Expression<Func<TEntity, bool>> IsSatisfiedBy();
    }
}
