namespace HitechCraft.DAL.Domain
{
    using Common.Entity;

    public class RulePoint : BaseEntity<RulePoint>
    {
        public virtual string Name { get; set; }
    }
}
