using HitechCraft.Core.Projector;


namespace HitechCraft.WebApplication.Mapper
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using AutoMapper;
    using AutoMapper.Mappers;
    using AutoMapper.QueryableExtensions;

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