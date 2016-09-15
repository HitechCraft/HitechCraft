using HitechCraft.Core.Models.Enum;

namespace HitechCraft.WebApplication.Models
{
    public class CurrencyTopViewModel
    {
        public int PlayerId { get; set; }

        public string PlayerName { get; set; }

        public Gender PlayerGender { get; set; }

        public double Gonts { get; set; }

        public double Rubles { get; set; }
    }
}