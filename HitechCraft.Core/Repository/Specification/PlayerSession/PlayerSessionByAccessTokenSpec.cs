using System;
using System.Linq.Expressions;

namespace HitechCraft.Core.Repository.Specification.PlayerSession
{
    #region Using Directives

    

    #endregion

    public class PlayerSessionByAccessTokenSpec : BaseSpecification<Entity.PlayerSession>
    {
        #region Private Fields

        private readonly string _accessTocken;

        #endregion

        #region Constructor

        public PlayerSessionByAccessTokenSpec(string accessToken)
        {
            this._accessTocken = accessToken;
        }

        #endregion

        #region Expression

        public override Expression<Func<Entity.PlayerSession, bool>> IsSatisfiedBy()
        {
            return playerSession => playerSession.Session == this._accessTocken;
        }

        #endregion
    }
}
