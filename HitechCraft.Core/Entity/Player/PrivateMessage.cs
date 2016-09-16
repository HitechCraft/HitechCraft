using HitechCraft.Core.Entity.Base;

namespace HitechCraft.Core.Entity
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    public class PrivateMessage : BaseEntity<PrivateMessage>
    {
        public virtual string Title { get; set; }

        public virtual string Text { get; set; }
        
        public virtual DateTime TimeCreate { get; set; }

        public virtual ISet<PMPlayerBox> PmPlayerBox { get; set; }
    }
}
