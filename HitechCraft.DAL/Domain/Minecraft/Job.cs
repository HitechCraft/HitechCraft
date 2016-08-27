namespace HitechCraft.DAL.Domain
{
    using Common.Entity;

    public class Job : BaseEntity<Job>
    {
        public virtual byte[] Uuid { get; set; }

        public virtual string PlayerName { get; set; }

        public virtual string JobName { get; set; }

        public virtual int Experiance { get; set; }

        public virtual int Level { get; set; }
    }
}
