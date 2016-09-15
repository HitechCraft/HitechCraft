namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using HitechCraft.Core.Entity;

    #endregion

    public class IKTransactionByPlayerNameSpec : ISpecification<IKTransaction>
    {
        #region Private Fields

        private readonly string _playerName;

        #endregion

        #region Constructor

        public IKTransactionByPlayerNameSpec(string playerName)
        {
            this._playerName = playerName;
        }

        #endregion

        #region Expression

        public Expression<Func<IKTransaction, bool>> IsSatisfiedBy()
        {
            return ikTransaction => ikTransaction.Player.Name == this._playerName;
        }

        #endregion
    }
}
