namespace HitechCraft.Projector.Impl.Extentions
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public static class ProjectorQueryExtentions
    {
        public static IQueryable<TResult> Project<TSource, TResult>(this IQueryable<TSource> collection, IProjector<TSource, TResult> projector)
        {
            return collection.Select(projector.ProjectExpr());
        }
        
        public static IEnumerable<TResult> Project<TSource, TResult>(this IEnumerable<TSource> collection, IProjector<TSource, TResult> projector)
        {
            return collection.Select(projector.ProjectFunc());
        }
    }
}
