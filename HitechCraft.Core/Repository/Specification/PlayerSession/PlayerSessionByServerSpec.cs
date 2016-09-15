using System;
using System.Linq.Expressions;

namespace HitechCraft.Core.Repository.Specification.PlayerSession
{
    #region Using Directives

    

    #endregion

    public class PlayerSessionByServerSpec : BaseSpecification<Entity.PlayerSession>
    {
        #region Private Fields

        private readonly string _serverId;

        #endregion

        #region Constructor

        public PlayerSessionByServerSpec(string serverId)
        {
            this._serverId = serverId;
        }

        #endregion

        #region Expression

        public override Expression<Func<Entity.PlayerSession, bool>> IsSatisfiedBy()
        {
            return playerSession => playerSession.Server == this._serverId;
        }

        #endregion
    }
}
