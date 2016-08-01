namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using Common.Repository.Specification;
    using Domain;

    #endregion

    public class AuthLogByPlayerNameSpec : ISpecification<AuthLog>
    {
        #region Private Fields

        private readonly string _playerName;

        #endregion

        #region Constructor

        public AuthLogByPlayerNameSpec(string playerName)
        {
            this._playerName = playerName;
        }

        #endregion

        #region Expression

        public Expression<Func<AuthLog, bool>> IsSatisfiedBy()
        {
            return authLog => authLog.Player.Name == this._playerName;
        }

        #endregion
    }
}
