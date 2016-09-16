using HitechCraft.Core.Entity.Base;

namespace HitechCraft.Core.Entity
{
    public class Rule : BaseEntity<Rule>
    {
        public virtual string Text { get; set; }

        public virtual RulePoint Point { get; set; }
    }
}
