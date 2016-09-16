using HitechCraft.Core.Entity.Base;

namespace HitechCraft.Core.Entity
{
    #region Using Directives

    using Models.Enum;

    #endregion

    /// <summary>
    /// Player model
    /// </summary>
    public class Player : BaseEntity<Player>
    {
        #region Properties

        /// <summary>
        /// Player name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Player gender
        /// </summary>
        public virtual Gender Gender { get; set; }

        /// <summary>
        /// Info about player
        /// </summary>
        public virtual PlayerInfo Info { get; set; }
        
        #endregion
    }
}