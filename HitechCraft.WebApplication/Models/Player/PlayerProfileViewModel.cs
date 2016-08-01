namespace HitechCraft.WebApplication.Models
{
    using Common.Models.Enum;

    public class PlayerProfileViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        //public PlayerGroup Group { get; set; }

        public Gender Gender { get; set; }
    }
}