namespace HitechCraft.Common.Projector
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;

    #endregion

    /// <summary>
    /// Base implementation of Projector
    /// </summary>
    /// <typeparam name="TSource">Source entity type</typeparam>
    /// <typeparam name="TResult">Result entity type</typeparam>
    public abstract class BaseProjector<TSource, TResult> : IProjector<TSource, TResult>
    {
        public abstract Expression<Func<TSource, TResult>> ProjectExpr();

        public Func<TSource, TResult> ProjectFunc()
        {
            return this.ProjectExpr().Compile();
        }

        public TResult Project(TSource source)
        {
            return this.ProjectFunc()(source);
        }
    }
}
