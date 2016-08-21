namespace HitechCraft.WebApplication.Models
{
    using System;

    public class PrivateMessageViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string AuthorName { get; set; }

        public string RecipientName { get; set; }

        public DateTime TimeCreate { get; set; }
    }
}