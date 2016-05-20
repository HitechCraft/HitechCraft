using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    using Domain;
    using System;

    public class BanEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string PlayerName { get; set; }
        
        [Required]
        public string Reason { get; set; }
        
        public string BannedBy { get; set; }
        
        public DateTime ActionTime { get; set; }

        public bool IsTemped { get; set; }
        
        public DateTime? TempTime { get; set; }

        [Required]
        public BanType Type { get; set; }
    }
}