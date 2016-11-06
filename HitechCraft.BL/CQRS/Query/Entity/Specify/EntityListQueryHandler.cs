﻿using HitechCraft.Core.Databases;
using HitechCraft.Core.Entity.Base;

namespace HitechCraft.BL.CQRS.Query.Specify
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using Core.DI;
    using Core.Entity;
    using DAL.Repository;

    #endregion

    public class EntityListQueryHandler<TEntity, TResult, TDataBase> 
        : IQueryHandler<EntityListQuery<TEntity, TResult>, ICollection<TResult>> where TEntity : BaseEntity<TEntity> where TDataBase : IDataBase
    {
        private readonly IContainer _container;

        public EntityListQueryHandler(IContainer container)
        {
            _container = container;
        }

        public ICollection<TResult> Handle(EntityListQuery<TEntity, TResult> query)
        {
            var entityRep = _container.Resolve<IRepository<TDataBase, TEntity>>();
            
            if(query.Projector == null)
                throw new Exception("Для получения объектов необходима проекция сущностей");

            return entityRep.Query(query.Specification, query.Projector);
        }
    }
}
