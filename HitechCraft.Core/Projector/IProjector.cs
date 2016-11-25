using System;
using System.Linq.Expressions;

namespace HitechCraft.Core.Projector
{
    #region Using Directives

    

    #endregion

    /// <summary>
    /// Projector intearface
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IProjector<TSource, TResult>
    {
        Expression<Func<TSource, TResult>> ProjectExpr();

        Func<TSource, TResult> ProjectFunc();

        TResult Project(TSource source);
    }
}
