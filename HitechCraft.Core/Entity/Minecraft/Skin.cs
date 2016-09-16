using HitechCraft.Core.Entity.Base;

namespace HitechCraft.Core.Entity
{
    using Models.Enum;

    public class Skin : BaseEntity<Skin>
    {
        public virtual string Name { get; set; }

        public virtual byte[] Image { get; set; }

        public virtual Gender Gender { get; set; }
    }
}
