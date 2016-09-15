namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using HitechCraft.Core.Entity;

    #endregion

    public class CommentByNewsIdSpec : ISpecification<Comment>
    {
        #region Private Fields

        private readonly int _newsId;

        #endregion

        #region Constructor

        public CommentByNewsIdSpec(int newsId)
        {
            this._newsId = newsId;
        }

        #endregion

        #region Expression

        public Expression<Func<Comment, bool>> IsSatisfiedBy()
        {
            return comment => comment.News.Id == this._newsId;
        }

        #endregion
    }
}
