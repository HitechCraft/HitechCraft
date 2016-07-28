namespace HitechCraft.Common.Repository
{
    #region Using Directives

    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using CQRS.Extentions;
    using DI;
    using Projector;
    using Specification;
    using Entity;
    using UnitOfWork;
    using NHibernate;
    using NHibernate.Linq;
    using System;
    #endregion

    public class BaseRepository<TEntity> : IRepository<TEntity> 
        where TEntity : BaseEntity<TEntity>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContainer _container;

        public BaseRepository(IContainer container)
        {
            this._container = container;
            this._unitOfWork = this._container.Resolve<IUnitOfWork>();
        }

        public TEntity GetEntity(object id)
        {
            return this._unitOfWork.Session.Get<TEntity>(id);
        }

        public void Add(TEntity entity)
        {
            this._unitOfWork.BeginTransaction();
            this._unitOfWork.Session.Save(entity);
        }

        public void Update(TEntity entity)
        {
            this._unitOfWork.BeginTransaction();
            this._unitOfWork.Session.Update(entity);
        }

        public void Delete(object id)
        {
            this._unitOfWork.BeginTransaction();
            this._unitOfWork.Session.Delete(this._unitOfWork.Session.Load<TEntity>(id));
        }

        public ICollection<TResult> Query<TResult>(ISpecification<TEntity> specification, IProjector<TEntity, TResult> projector)
        {
            var entities = this._unitOfWork.Session.Query<TEntity>();
            
            if (specification != null) entities = entities.Where(specification.IsSatisfiedBy());

            if (projector != null)
            {
                return entities.Project(projector).ToList();
            }

            //С коллекцией ругается...
            return ((IQueryable<TResult>)entities).ToList();
        }

        public ICollection<TEntity> Query(ISpecification<TEntity> specification)
        {
            return this.Query<TEntity>(specification, null);
        }

        public ICollection<TResult> Query<TResult>(IProjector<TEntity, TResult> projector)
        {
            return this.Query(null, projector);
        }

        public ICollection Query()
        {
            return this.Query<TEntity>(null, null).ToList();
        }

        public void Dispose()
        {
            this._unitOfWork.Commit();
        }
    }
}
