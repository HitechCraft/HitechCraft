﻿using HitechCraft.Core.Entity.Base;

namespace HitechCraft.Core.Entity
{
    using System.Collections.Generic;

    public class RulePoint : BaseEntity<RulePoint>
    {
        public virtual string Name { get; set; }

        public virtual ISet<Rule> Rules { get; set; }
    }
}
