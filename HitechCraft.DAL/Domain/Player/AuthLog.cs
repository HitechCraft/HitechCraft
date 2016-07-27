namespace HitechCraft.DAL.Domain
{
    using Common.Entity;
    using System;
    using Common.Models.Enum;

    public class AuthLog : BaseEntity<AuthLog>
    {
        public Player Player { get; set; }

        public string Ip { get; set; }

        public string Browser { get; set; }

        public AuthLogType Type { get; set; }

        public DateTime Time { get; set; }
    }
}
