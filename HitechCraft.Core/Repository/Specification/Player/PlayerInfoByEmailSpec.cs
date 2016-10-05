namespace HitechCraft.Core.Repository.Specification.Player
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;

    #endregion

    public class PlayerInfoByEmailSpec : ISpecification<Entity.PlayerInfo>
    {
        #region Private Fields

        private readonly string _email;

        #endregion

        #region Constructor

        public PlayerInfoByEmailSpec(string email)
        {
            this._email = email;
        }

        #endregion

        #region Expression

        public Expression<Func<Entity.PlayerInfo, bool>> IsSatisfiedBy()
        {
            return playerInfo => playerInfo.Email == this._email;
        }

        #endregion
    }
}
