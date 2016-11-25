using System.Collections.Generic;
using System.Linq;

namespace HitechCraft.Core.Projector.Extentions
{
    #region Using Directives

    

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

        public static ICollection<TResult> Project<TSource, TResult>(this ICollection<TSource> collection, IProjector<TSource, TResult> projector)
        {
            return collection.Select(projector.ProjectFunc()).ToList();
        }
    }
}
