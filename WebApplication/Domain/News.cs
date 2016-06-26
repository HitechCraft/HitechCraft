namespace WebApplication.Domain
{
    using System;
    using System.Web.Mvc;

    [Bind(Include = "Author")]
    public class News
    {
        /// <summary>
        /// News id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// News Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// News body
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// News image
        /// </summary>
        public byte[] Image { get; set; }
        /// <summary>
        /// News additing time
        /// </summary>
        public DateTime TimeCreate { get; set; }
        /// <summary>
        /// News author
        /// </summary>
        public ApplicationUser Author { get; set; }
        /// <summary>
        /// Count views
        /// </summary>
        public int ViewersCount { get; set; }
    }
}