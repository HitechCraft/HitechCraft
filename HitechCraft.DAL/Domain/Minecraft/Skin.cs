namespace HitechCraft.DAL.Domain
{
    using Common.Entity;

    public class Skin : BaseEntity<Skin>
    {
        public virtual string Name { get; set; }

        public virtual byte[] Image { get; set; }
    }
}
