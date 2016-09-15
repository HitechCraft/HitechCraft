namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using HitechCraft.Core.Entity;

    #endregion

    public class PlayerSessionByPlayerProfileSpec : BaseSpecification<PlayerSession>
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

        public override Expression<Func<PlayerSession, bool>> IsSatisfiedBy()
        {
            return playerSession => playerSession.Md5 == this._playerProfile;
        }

        #endregion
    }
}
