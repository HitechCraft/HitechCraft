using System;
using System.Linq.Expressions;

namespace HitechCraft.Core.Repository.Specification.PlayerSession
{
    #region Using Directives

    

    #endregion

    public class PlayerSessionByPlayerNameSpec : BaseSpecification<Entity.PlayerSession>
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

        public override Expression<Func<Entity.PlayerSession, bool>> IsSatisfiedBy()
        {
            return playerSession => playerSession.Player.Name == this._playerName;
        }

        #endregion
    }
}
