namespace WebApplication.Models
{
    #region Using Directives
    
    using System;

    #endregion

    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public DateTime TimeCreate { get; set; }
    }
}