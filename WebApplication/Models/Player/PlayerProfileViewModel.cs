namespace WebApplication.Models
{
    using Domain;

    public class PlayerProfileViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public PlayerStatus Status { get; set; }

        public Gender Gender { get; set; }

        public float Rubles { get; set; }

        public float Gonts { get; set; }
    }
}