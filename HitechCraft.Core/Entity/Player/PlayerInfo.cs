using HitechCraft.Core.Entity.Base;

namespace HitechCraft.Core.Entity
{
    public class PlayerInfo : BaseEntity<PlayerInfo>
    {
        public virtual string Email { get; set; }

        public virtual Player Refer { get; set; }
    }
}
