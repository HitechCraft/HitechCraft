﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    using Domain;
    using System;

    public class BanViewModel
    {
        public int Id { get; set; }

        public string PlayerName { get; set; }
        
        public string Reason { get; set; }
        
        public string BannedBy { get; set; }
        
        public DateTime ActionTime { get; set; }
        
        public DateTime TempTime { get; set; }

        public BanType Type { get; set; }
    }
}