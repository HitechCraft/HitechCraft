using System.Collections.Generic;

namespace HitechCraft.WebApplication.Models
{
    using System;

    public class PrivateMessageViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public IEnumerable<PMPlayerViewModel> Players { get; set; }

        public DateTime TimeCreate { get; set; }
    }
}