namespace HitechCraft.WebAdmin.Models
{
    using Common.Models.Enum;

    public class PlayerInfoViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Gender Gender { get; set; }
        
        public double Gonts { get; set; }

        public double Rubles { get; set; }
    }
}