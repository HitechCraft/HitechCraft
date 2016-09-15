using HitechCraft.Core.Models.Enum;

namespace HitechCraft.WebApplication.Models
{
    using System;

    public class BanUnbanViewModel
    {
        public int Id { get; set; }
        
        public string PlayerName { get; set; }
        
        public DateTime ActionTime { get; set; }

        public BanType Type { get; set; }
    }
}