namespace HitechCraft.WebApplication.Models
{
    using System;

    public class AuthLogViewModel
    {
        public string Id { get; set; }

        public string Ip { get; set; }

        public string Browser { get; set; }

        public DateTime Time { get; set; }
    }
}