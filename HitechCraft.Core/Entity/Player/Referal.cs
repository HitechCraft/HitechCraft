using HitechCraft.Core.Entity.Base;

namespace HitechCraft.Core.Entity
{
    public class Referal : BaseEntity<Referal>
    {
        /// <summary>
        /// Игрок, который привел участника
        /// </summary>
        public virtual Player Refer { get; set; }

        /// <summary>
        /// Приведенный участник
        /// </summary>
        public virtual Player Referer { get; set; }
    }
}
