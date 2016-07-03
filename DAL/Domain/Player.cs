namespace DAL.Domain
{
    /// <summary>
    /// Use in pex and aspnetroles relations
    /// </summary>
    public enum PlayerGroup
    {
        Player, Helper, Moderator, Administrator
    }

    /// <summary>
    /// Player sex (gender) enum
    /// </summary>
    public enum Gender
    {
        Male, Female
    }

    /// <summary>
    /// Player model
    /// </summary>
    public class Player
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

        #endregion
    }
}