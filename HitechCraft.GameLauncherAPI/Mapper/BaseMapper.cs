﻿using System;
using System.Linq.Expressions;
using HitechCraft.Common.Projector;

namespace HitechCraft.GameLauncherAPI.Mapper
{
    #region Using Directives

    

    #endregion

    public class BaseMapper<TSource, TResult> : BaseProjector<TSource,TResult>
    {
        #region Properties

        protected ConfigurationStore ConfigurationStore { get; set; }

        private MappingEngine MappingEngine { get; set; }

        #endregion

        #region Constructor

        protected BaseMapper()
        {
            this.ConfigurationStore = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);
            this.MappingEngine = new MappingEngine(this.ConfigurationStore);
        }

        #endregion

        public override Expression<Func<TSource, TResult>> ProjectExpr()
        {
            return this.MappingEngine.CreateMapExpression<TSource, TResult>();
        }
    }
}