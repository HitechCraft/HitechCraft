namespace WebApplication.Domain
{
    #region Using Directives

    using System;

    #endregion

    public class Comment
    {
        /// <summary>
        /// Object Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Comment text
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Comment author
        /// </summary>
        public ApplicationUser Author { get; set; }
        /// <summary>
        /// Comment news
        /// </summary>
        public News News { get; set; }
        /// <summary>
        /// Comment time create
        /// </summary>
        public DateTime TimeCreate { get; set; }
    }
}