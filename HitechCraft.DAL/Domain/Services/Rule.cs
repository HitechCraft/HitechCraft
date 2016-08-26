namespace HitechCraft.DAL.Domain
{
    using Common.Entity;

    public class Rule : BaseEntity<Rule>
    {
        public virtual string Text { get; set; }

        public virtual RulePoint Point { get; set; }
    }
}
