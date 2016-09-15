using System;
using System.Linq.Expressions;
using HitechCraft.Core.Entity;

namespace HitechCraft.Core.Repository.Specification.Player
{
    #region Using Directives

    

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
