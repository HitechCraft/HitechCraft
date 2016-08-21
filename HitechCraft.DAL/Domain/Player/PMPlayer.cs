namespace HitechCraft.DAL.Domain
{
    using Common.Models.Enum;
    using Common.Entity;

    public class PMPlayer : BaseEntity<PMPlayer>
    {
        public virtual PrivateMessage Message { get; set; }

        public virtual Player Player { get; set; }

        public virtual PMPlayerType PlayerType { get; set; }

        public virtual PMType PmType { get; set; }
    }
}
