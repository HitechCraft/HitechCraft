using System;
using System.Linq.Expressions;

namespace HitechCraft.Core.Repository.Specification.PlayerSession
{
    #region Using Directives

    

    #endregion

    public class PlayerSessionByPlayerProfileSpec : BaseSpecification<Entity.PlayerSession>
    {
        #region Private Fields

        private readonly string _playerProfile;

        #endregion

        #region Constructor

        public PlayerSessionByPlayerProfileSpec(string playerProfile)
        {
            this._playerProfile = playerProfile;
        }

        #endregion

        #region Expression

        public override Expression<Func<Entity.PlayerSession, bool>> IsSatisfiedBy()
        {
            return playerSession => playerSession.Md5 == this._playerProfile;
        }

        #endregion
    }
}
