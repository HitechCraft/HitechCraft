namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using HitechCraft.Core.Entity;

    #endregion

    public class PlayerSessionByServerSpec : BaseSpecification<PlayerSession>
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

        public override Expression<Func<PlayerSession, bool>> IsSatisfiedBy()
        {
            return playerSession => playerSession.Server == this._serverId;
        }

        #endregion
    }
}
