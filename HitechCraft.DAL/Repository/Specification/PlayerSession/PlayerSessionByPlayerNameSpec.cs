namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using Common.Repository.Specification;
    using Domain;

    #endregion

    public class PlayerSessionByPlayerNameSpec : BaseSpecification<PlayerSession>
    {
        #region Private Fields

        private readonly string _playerName;

        #endregion

        #region Constructor

        public PlayerSessionByPlayerNameSpec(string playerName)
        {
            this._playerName = playerName;
        }

        #endregion

        #region Expression

        public override Expression<Func<PlayerSession, bool>> IsSatisfiedBy()
        {
            return playerSession => playerSession.Player.Name == this._playerName;
        }

        #endregion
    }
}
