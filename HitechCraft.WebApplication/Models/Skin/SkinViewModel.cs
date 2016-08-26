namespace HitechCraft.WebApplication.Models
{
    using Common.Models.Enum;

    public class SkinViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public Gender Gender { get; set; }
    }
}