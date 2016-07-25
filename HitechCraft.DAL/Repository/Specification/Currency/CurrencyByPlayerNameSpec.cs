namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using Common.Repository.Specification;
    using Domain;

    #endregion

    public class CurrencyByPlayerNameSpec : ISpecification<Currency>
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

        public Expression<Func<Currency, bool>> IsSatisfiedBy()
        {
            return currency => currency.Player.Name == this._playerName;
        }

        #endregion
    }
}
