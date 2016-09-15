using HitechCraft.Core.Models.Enum;

namespace HitechCraft.WebAdmin.Models
{
    public class SkinViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public Gender Gender { get; set; }
    }
}