using HitechCraft.Core.Models.Enum;

namespace HitechCraft.WebApplication.Models
{
    public class PlayerProfileViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        //public PlayerGroup Group { get; set; }

        public Gender Gender { get; set; }
    }
}