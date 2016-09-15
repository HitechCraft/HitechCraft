using System;
using System.Linq.Expressions;

namespace HitechCraft.Core.Repository.Specification.Currency
{
    #region Using Directives

    

    #endregion

    public class CurrencyByPlayerNameSpec : ISpecification<Entity.Currency>
    {
        #region Private Fields

        private readonly string _playerName;

        #endregion

        #region Constructor

        public CurrencyByPlayerNameSpec(string playerName)
        {
            this._playerName = playerName;
        }

        #endregion

        #region Expression

        public Expression<Func<Entity.Currency, bool>> IsSatisfiedBy()
        {
            return currency => currency.Player.Name == this._playerName;
        }

        #endregion
    }
}
