namespace HitechCraft.DAL.Domain
{
    using Common.Entity;

    public class Skin : BaseEntity<Skin>
    {
        public string Name { get; set; }

        public byte[] Image { get; set; }
    }
}
