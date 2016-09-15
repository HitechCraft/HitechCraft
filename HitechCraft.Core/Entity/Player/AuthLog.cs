namespace HitechCraft.Core.Entity
{
    using System;

    public class AuthLog : BaseEntity<AuthLog>
    {
        public virtual Player Player { get; set; }

        public virtual string Ip { get; set; }

        public virtual string Browser { get; set; }
        
        public virtual DateTime Time { get; set; }
    }
}
