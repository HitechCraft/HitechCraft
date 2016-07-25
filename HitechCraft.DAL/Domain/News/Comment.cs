namespace HitechCraft.DAL.Domain
{
    #region Using Directives

    using System;
    using Common.Entity;

    #endregion

    public class Comment : BaseEntity<Comment>
    {
        #region Properties
        
        /// <summary>
        /// Comment text
        /// </summary>
        public virtual string Text { get; set; }

        /// <summary>
        /// Comment author
        /// </summary>
        public virtual Player Author { get; set; }

        /// <summary>
        /// Comment news
        /// </summary>
        public virtual News News { get; set; }

        /// <summary>
        /// Comment time create
        /// </summary>
        public virtual DateTime TimeCreate { get; set; }

        #endregion
    }
}