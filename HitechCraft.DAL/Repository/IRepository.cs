namespace HitechCraft.DAL.Repository
{
    #region Using Directives

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Core.Entity;
    using Specification;
    using Projector.Impl;

    #endregion

    public interface IRepository<TEntity> : IDisposable where TEntity : BaseEntity<TEntity> 
    {
        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns></returns>
        TEntity GetEntity(object id);
        /// <summary>
        /// Add new entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Add(TEntity entity);
        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(TEntity entity);
        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">Entity id</param>
        void Delete(object id);
        /// <summary>
        /// Queriable entity collection
        /// </summary>
        /// <returns></returns>
        ICollection Query();
        /// <summary>
        /// Getting entity collection by spec
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        ICollection<TEntity> Query(ISpecification<TEntity> specification);
        /// <summary>
        /// Getting entity collection by projector
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="projector"></param>
        /// <returns></returns>
        ICollection<TResult> Query<TResult>(IProjector<TEntity, TResult> projector);
        /// <summary>
        /// Getting entity collection by specification and projector
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="specification"></param>
        /// <param name="projector"></param>
        /// <returns></returns>
        ICollection<TResult> Query<TResult>(ISpecification<TEntity> specification, IProjector<TEntity, TResult> projector);
    }
}
