namespace HitechCraft.DAL.Domain
{
    using Common.Models.Enum;
    using Common.Entity;

    public class Skin : BaseEntity<Skin>
    {
        public virtual string Name { get; set; }

        public virtual byte[] Image { get; set; }

        public virtual Gender Gender { get; set; }
    }
}
