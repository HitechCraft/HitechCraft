﻿namespace WebApplication.Models
{
    #region Using Directives

    using System;

    #endregion

    public class NewsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public byte[] Image { get; set; }

        public DateTime TimeCreate { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }
        
        public int ViewersCount { get; set; }
    }
}