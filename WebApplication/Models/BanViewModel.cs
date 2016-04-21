using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    using Domain;
    using System;

    public class BanViewModel
    {
        public int Id { get; set; }

        public string PlayerName { get; set; }
        
        [Required]
        public string Reason { get; set; }
        
        [Required]
        public string BannedBy { get; set; }
        
        public DateTime ActionTime { get; set; }
        
        public DateTime TempTime { get; set; }

        public BanType Type { get; set; }
    }
}