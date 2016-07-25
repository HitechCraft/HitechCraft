namespace HitechCraft.DAL.Domain
{
    using Common.Entity;

    public class PlayerInfo : BaseEntity<PlayerInfo>
    {
        public virtual string Email { get; set; }
    }
}
